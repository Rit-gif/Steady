using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startHealth = 100;
    protected float Health { get; private set; }
    protected bool IsDead;

    public event Action OnDeath;
    public event Action<float> OnHealthChanged; // sends 0..1

    protected virtual void Start()
    {
        Health = startHealth;
        OnHealthChanged?.Invoke(1f);
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead) return;

        Health = Mathf.Max(Health - damage, 0);
        OnHealthChanged?.Invoke(Health / startHealth);

        if (Health > 0) return;

        IsDead = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}