using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startHealth;
    protected float Health { get; private set; }
    protected bool IsDead;

    public event Action OnDeath;
    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if(Health >0 || IsDead)
        {
            return;
        }

        IsDead = true;
        OnDeath?.Invoke();

        Destroy(gameObject);
    }
}
