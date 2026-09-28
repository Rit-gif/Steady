using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.wholeNumbers = true;
            healthBar.value = currentHealth;
        }
        else
        {
            Debug.LogError("PlayerHealth: healthBar is not assigned!");
        }
    }

    void Update()
    {
        // Press H to test damage
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        Debug.Log("HP: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;   // stops Play mode in the editor
#else
    Application.Quit();                    // closes a built game
#endif
    }
}