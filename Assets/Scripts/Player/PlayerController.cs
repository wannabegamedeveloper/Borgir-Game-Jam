using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rayCastDistance;
    [SerializeField] private float speed;
    
    private bool moving;
    private float angle = 90f;
    private Rigidbody rb;

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
        moving = false;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision other)
    {
        moving = false;
        rb.velocity = Vector3.zero;
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
            RotateToDirection((int) Directions.Right);
        else if (Input.GetKeyDown(KeyCode.A))
            RotateToDirection((int) Directions.Left);
        else if (Input.GetKeyDown(KeyCode.W))
            RotateToDirection((int) Directions.Up);
        else if (Input.GetKeyDown(KeyCode.S))
            RotateToDirection((int) Directions.Down);
    }

    private void RotateToDirection(float direction)
    {
        moving = true;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * direction));
    }
    
    private enum Directions
    {
        Right = 0,
        Left = 2,
        Up = 1,
        Down = 3
    }
}
