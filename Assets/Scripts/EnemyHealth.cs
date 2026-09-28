using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public UnityEvent<float> onHealthChanged; // sends 0..1
    int current;

    void Awake() { current = maxHealth; }

    public void TakeDamage(int amount)
    {
        current = Mathf.Max(current - amount, 0);
        onHealthChanged?.Invoke((float)current / maxHealth);
        if (current == 0) Destroy(gameObject);
    }
}