using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health;

    private MeshRenderer mesh;

    [SerializeField] private float maxHealth;
    private Color oldColor;

    public void TakeDamage(float damage)
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
        mesh = GetComponentInChildren<MeshRenderer>();
        oldColor = mesh.material.color;
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
        mesh.material.color = new Color(255, 255, 255, 255);
        StartCoroutine(HitFlashEffect(mesh));
    }

    private IEnumerator HitFlashEffect(MeshRenderer mesh)
    {
        yield return new WaitForSeconds(0.1f);
        mesh.material.color = oldColor;
    }
}
