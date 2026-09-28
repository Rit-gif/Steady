using System.Collections;
using System;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Animator anim;
    protected Collider2D col;
    protected SpriteRenderer sr;
    private Rigidbody2D rb;
    private float xInput;
    private bool canMove = true;
    private bool canJump = true;


    [Header("HP")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damage_feed = 1f;
    private Coroutine damage_feedCoroutine;


    [Header("Attack details")]
    [SerializeField] private float attack_radius;
    [SerializeField] private Transform attack_point;
    [SerializeField] private LayerMask whatIsEnemy;


    [Header("Movement details")]
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float jumpPower = 7;
    [SerializeField] private bool isFacingRight = true;


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
        col = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;
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
            enemy.GetComponent<enemy_script>().take_damage();
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
        }
    }

    private void player_take_dmg()
    {
        currentHealth = currentHealth - 1;
        
        dmg_feed_play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void dmg_feed_play()
    {
        if (damage_feedCoroutine != null) StopCoroutine(damage_feedCoroutine);
        StartCoroutine(Damage_feedCo());
    }

    private IEnumerator Damage_feedCo()
    {
        Material origanalMat = sr.material;
        sr.material = damageMaterial;
        yield return new WaitForSeconds(damage_feed);
        sr.material = origanalMat;
    }

    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);
    }

    private void player_Movement()
    {
        if(canMove) rb.linearVelocity = new Vector2(xInput * move_speed, rb.linearVelocity.y);
        else rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
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
        if (isGrounded && canJump) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -ground_check_distance));
        Gizmos.DrawWireSphere(attack_point.position, attack_radius);
    }

}
