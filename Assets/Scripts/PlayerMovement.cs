using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hi Eric, this code isn't that clean but it's all temporary, change if you want

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speedX;
    public float speedY;

    public float maxSpeedX;
    public float maxSpeedY;

    public float friction;

    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);
    }

    void FixedUpdate()
    {

        if (moveLeft)
        {
            rb.AddForce(Vector2.left * speedX);
        }
        if (moveRight)
        {
            rb.AddForce(Vector2.right * speedX);
        }
        if (moveUp)
        {
            rb.AddForce(Vector2.up * speedY);
        }
        if (moveDown)
        {
            rb.AddForce(Vector2.down * speedY);
        }

        // Stuff 

        if (moveLeft && moveRight)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * friction, rb.linearVelocity.y);
        }

        if (moveUp && moveDown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * friction);
        }

        if (!moveLeft && !moveRight)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * friction, rb.linearVelocity.y);
        }

        if (!moveUp && !moveDown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * friction);
        }

        if (rb.linearVelocity.magnitude > maxSpeedX)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * (maxSpeedX);
        }

    }
}
