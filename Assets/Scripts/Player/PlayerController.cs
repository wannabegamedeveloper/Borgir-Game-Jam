using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider[] boxColliders;
    [SerializeField] private float rayDistance;
    [SerializeField] private KeyCode[] key;
    
    
    private bool moving;
    private float angle = 90f;
    private Rigidbody rb;
    private int lastDirection = -1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!moving)
        {
            ReadInput();
            return;
        }
        
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        print("test");
        moving = false;
        rb.velocity = Vector3.zero;
        other.transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(key[0]) && !Physics.Raycast(transform.position, Vector3.right, rayDistance))
            RotateToDirection((int) Directions.Right);
        else if (Input.GetKeyDown(key[1]) && !Physics.Raycast(transform.position, Vector3.left, rayDistance))
            RotateToDirection((int) Directions.Left);
        else if (Input.GetKeyDown(key[2]) && !Physics.Raycast(transform.position, Vector3.up, rayDistance))
            RotateToDirection((int) Directions.Up);
        else if (Input.GetKeyDown(key[3]) && !Physics.Raycast(transform.position, Vector3.down, rayDistance))
            RotateToDirection((int) Directions.Down);
    }

    private void RotateToDirection(int direction)
    {
        if (lastDirection == -1 || lastDirection != direction)
        {
            lastDirection = direction;
            moving = true;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * direction));
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
}
