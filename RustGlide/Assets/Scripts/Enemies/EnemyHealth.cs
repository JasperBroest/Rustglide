using System.Collections;
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
        else
        {
            HitFlash();
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

    private void HitFlash()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material.color = new Color(255, 255, 255, 255);
        StartCoroutine(HitFlashEffect(mesh));
    }

    private IEnumerator HitFlashEffect(MeshRenderer mesh)
    {
        yield return new WaitForSeconds(0.2f);
        mesh.material.color = new Color(255, 0, 0, 100);
    }
}
