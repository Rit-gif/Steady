using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float cooldown = 1f;
    float nextHitTime;

    void OnCollisionStay2D(Collision2D col)
    {
        if (Time.time < nextHitTime) return;

        if (col.gameObject.TryGetComponent(out PlayerHealth health))
        {
            health.TakeDamage(damage);
            nextHitTime = Time.time + cooldown;
        }
    }
}