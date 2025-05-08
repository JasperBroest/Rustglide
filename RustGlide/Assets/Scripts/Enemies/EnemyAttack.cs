using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private int dmg;
    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Damage
                Debug.Log("Deal damage");
                Debug.Log(PlayerHealth.Instance);
                PlayerHealth.Instance.TakeDamage(dmg);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
    }
}
