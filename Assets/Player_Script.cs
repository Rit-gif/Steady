using UnityEngine;

public class Player_Script : MonoBehaviour
{

    // variable
    // physics
    private Rigidbody2D rb;
    private Animator anim;
    

    // movements, 8 directions
    private Vector2 moveInput;
    [SerializeField] private float movementSpeed = 3;

    // [SerializeField] private float dashSpeed = 5;
    //private float isMoving = 0;


    
    // collisions



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponentInChildren<Animator>();
    }


    private void Start()
    {
        
    }


    private void Update()
    {
        player_Input();
        player_Movement();
        // playerAnimation();
    }


    private void player_Input()
    {
        // xInput = Input.GetAxisRaw("Horizontal");
        // if (Input.GetKeyDown(KeyCode.Space)) dash();

    }

    private void player_Movement()
    {
        // rb.linearVelocity = new Vector2(xInput * movementSpeed, rb.linearVelocity.y);
        
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        if(Horizontal == 0 && Vertical == 0)
        {
            rb.linearVelocity = new Vector2(0, 0);
            return;
        }

        moveInput = new Vector2(Horizontal, Vertical);
        rb.linearVelocity = moveInput * movementSpeed * Time.fixedDeltaTime;
    }

    private void dash()
    {

    }

    private void Animate()
    {

    }
}
