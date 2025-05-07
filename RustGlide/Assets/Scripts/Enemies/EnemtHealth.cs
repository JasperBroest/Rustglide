using UnityEngine;

public class EnemtHealth : MonoBehaviour
{
    public int Health;

    [SerializeField] private int maxHealth;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        Health = maxHealth;
    }
    private void Die()
    {
        // Effect
        Destroy(gameObject);
    }
}
