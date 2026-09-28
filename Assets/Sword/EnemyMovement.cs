using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float chaseRange = 6f;
    [SerializeField] float patrolDistance = 3f;
    [SerializeField] Transform player;

    Rigidbody2D rb;
    Animation anim;
    Vector2 startPos;
    int direction = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        float dist = player != null ? Vector2.Distance(transform.position, player.position) : Mathf.Infinity;

        if (dist < chaseRange)
        {
            direction = player.position.x > transform.position.x ? 1 : -1;
        }
        else
        {
            if (transform.position.x > startPos.x + patrolDistance) direction = -1;
            else if (transform.position.x < startPos.x - patrolDistance) direction = 1;
        }

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;

        string clip = Mathf.Abs(rb.linearVelocity.x) > 0.1f ? "Movement" : "Idle";
        if (!anim.IsPlaying(clip))
            anim.CrossFade(clip, 0.1f);
    }
}