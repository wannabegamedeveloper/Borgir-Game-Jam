using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public bool allowInput = true;
    public int moves;
   
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider[] boxColliders;
    [SerializeField] private float rayDistance;
    [SerializeField] private KeyCode[] key;
    [SerializeField] private Text movesText;
    [SerializeField] private TurnBased turnBased;
    [SerializeField] private CheckWin checkWin;
    [SerializeField] private GameObject retryUI;
    [SerializeField] private PlayerController otherController;
    [SerializeField] private Ball ballType;
    
    
    private InputActions inputActions;
    private bool moving;
    private float angle = 90f;
    private Rigidbody rb;
    private bool triggeredObjective;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (ballType == Ball.Blue)
            inputActions.BlueController.Move.performed += Move;
        else
            inputActions.PinkController.Move.performed += Move;

        inputActions.ExtraInputs.Restart.performed += RestartGame;
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (allowInput && !moving)
            RotateToDirection(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }
    
    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void RestartGame(InputAction.CallbackContext callbackContext)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        movesText.transform.GetChild(0).gameObject.SetActive(allowInput);
        
        if (!moving)
            ReadInput();
    }

    private void FixedUpdate()
    {
        if (!moving) return;
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            if (!triggeredObjective)
            {
                triggeredObjective = true;
                checkWin.ballsStatus++;
                checkWin.CheckStatus();
                if (checkWin.ballsStatus == 1)
                    checkWin.checkBallStatus++;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        moving = false;
        rb.velocity = Vector3.zero;
        other.transform.GetComponent<BoxCollider>().enabled = false;
        if (checkWin.checkBallStatus == 2 && checkWin.ballsStatus == 1)
            RetryLevel();
        if (checkWin.checkBallStatus == 1 && checkWin.ballsStatus == 1)
            checkWin.checkBallStatus++;
    }

    private void RetryLevel()
    {
        retryUI.SetActive(true);
        enabled = false;
        otherController.enabled = false;
    }
    
    private void ReadInput()
    {
        if (allowInput)
        {
            // if (Input.GetKeyDown(key[0]) && !Physics.Raycast(transform.position, Vector3.right, rayDistance))
            //     RotateToDirection((int) Directions.Right);
            // else if (Input.GetKeyDown(key[1]) && !Physics.Raycast(transform.position, Vector3.left, rayDistance))
            //     RotateToDirection((int) Directions.Left);
            // else if (Input.GetKeyDown(key[2]) && !Physics.Raycast(transform.position, Vector3.up, rayDistance))
            //     RotateToDirection((int) Directions.Up);
            // else if (Input.GetKeyDown(key[3]) && !Physics.Raycast(transform.position, Vector3.down, rayDistance))
            //     RotateToDirection((int) Directions.Down);
        }
    }

    private void RotateToDirection(float x, float y)
    {
        Vector3 position = transform.position;
        bool hasReachedEdgeX = Physics.Raycast(position, new Vector3(x, 0f, 0f), rayDistance);
        bool hasReachedEdgeY = Physics.Raycast(position, new Vector3(0f, y, 0f), rayDistance);

        print(x);
        
        if (!hasReachedEdgeX && !hasReachedEdgeY)
        {
            turnBased.ChangeTurn(this);
            moves++;
            movesText.text = moves.ToString();
            moving = true;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle * (x - 1f), angle * y));
            foreach (var boxCollider in boxColliders)
                boxCollider.transform.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private enum Directions
    {
        Right = 0,
        Left = 2,
        Up = 1,
        Down = 3
    }

    private enum Ball
    {
        Pink = 1,
        Blue = 2
    }
}
