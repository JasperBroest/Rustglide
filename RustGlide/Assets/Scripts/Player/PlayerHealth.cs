using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health;

    private int maxHealth = 50;
    private GameObject playerSpawn;

    public static PlayerHealth Instance;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log(Health);
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Health = maxHealth;
        playerSpawn = GameObject.Find("PlayerSpawn");
    }


    private void Die()
    {
        transform.position = playerSpawn.transform.position;
    }
}
