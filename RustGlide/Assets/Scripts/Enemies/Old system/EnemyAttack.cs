using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private int dmg;

    private bool canAttack = true;
    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (canAttack)
                {
                    canAttack = false;
                    StartCoroutine(AttackCooldown());

                    // Damage
                    hit.collider.GetComponentInChildren<StaminaBar>().TakeDamage(dmg);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
}
