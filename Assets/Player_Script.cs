using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float xInput;


    [Header("Movement details")]
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private bool isFacingRight = true;


    [Header("Collision details")]
    [SerializeField] private float ground_check_distance;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;


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
        player_flip();
        HandleCollision();
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

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, ground_check_distance, whatIsGround);
    }

    private void player_flip()
    {
        if (rb.linearVelocity.x > 0 && isFacingRight == false) flip();
        else if(rb.linearVelocity.x < 0 && isFacingRight == true) flip(); 
    }

    private void flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void jump()
    {
        if (isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }

    
}
