using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
        EnemyManager.Instance.enemyList.Remove(gameObject);
        EnemyManager.Instance.EnemiesClearedCheck();
        Destroy(gameObject);
    }
}
