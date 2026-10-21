using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
            return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        Debug.Log("Player Health: " + CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Debug.Log("Player defeated!");
        }
    }
}
