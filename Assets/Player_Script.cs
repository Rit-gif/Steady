using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float xInput;
    private bool canMove = true;
    private bool canJump = true;

    [Header("Attack details")]
    [SerializeField] private float attack_radius;
    [SerializeField] private Transform attack_point;
    [SerializeField] private LayerMask whatIsEnemy;

    [Header("Attack Sound")]
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private AudioClip attackSound;

    [Header("Movement details")]
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private bool isFacingRight = true;

    [Header("Jump Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;

    [Header("Run Sound")]
    [SerializeField] private AudioSource runAudioSource;
    [SerializeField] private AudioClip runSound;


    [Header("Collision details")]
    [SerializeField] private float ground_check_distance;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;


    public void enable_jump_movement(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        player_Input();
        player_Movement();
        player_Animation();
        player_Flip();
        player_Collision();
    }

    public void damage_enemies()
    {
        Collider2D[] enemy_colliders = Physics2D.OverlapCircleAll(attack_point.position, attack_radius, whatIsEnemy);
        foreach (Collider2D enemy in enemy_colliders)
        {
            if (enemy.TryGetComponent(out IDamageable target))
                target.TakeDamage(10);
        }

    }

    private void player_Animation()
    {
        anim.SetFloat("x_velocity", rb.linearVelocity.x);
        anim.SetFloat("y_velocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void player_Input()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) player_jump_attempt();
        if (Input.GetKeyDown(KeyCode.Mouse0)) player_attack_attempt();
    }

    private void player_attack_attempt()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");

            if (attackAudioSource != null && attackSound != null)
            {
                attackAudioSource.PlayOneShot(attackSound);
            }
        }
    }

    private void player_Movement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(xInput * move_speed, rb.linearVelocity.y);

            if (isGrounded && xInput != 0)
            {
                if (!runAudioSource.isPlaying)
                {
                    runAudioSource.Play();
                }
            }
            else
            {
                if (runAudioSource.isPlaying)
                {
                    runAudioSource.Stop();
                }
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (runAudioSource.isPlaying)
            {
                runAudioSource.Stop();
            }
        }
    }

    private void player_Collision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, ground_check_distance, whatIsGround);
    }

    private void player_Flip()
    {
        if (rb.linearVelocity.x > 0 && isFacingRight == false) flip();
        else if (rb.linearVelocity.x < 0 && isFacingRight == true) flip();
    }

    private void flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void player_jump_attempt()
    {
        if (isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -ground_check_distance));
        Gizmos.DrawWireSphere(attack_point.position, attack_radius);
    }

}
