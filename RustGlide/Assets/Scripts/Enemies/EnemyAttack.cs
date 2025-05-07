using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Damage
                Debug.Log("Take damage");
            }
        }
    }
}
