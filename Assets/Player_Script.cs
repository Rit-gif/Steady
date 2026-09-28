using System;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float xInput;
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float jumpPower = 5;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) jump();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * move_speed, rb.linearVelocity.y);
    }

    private void jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        //if (isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }
}
