using System.Collections;
using UnityEngine;

public class AttackState : IState
{
    private bool canAttack = true;

    public void OnEnter(StateController controller)
    {
        // "What was that!?"
    }

    public void UpdateState(StateController controller)
    {
        Vector3 boxCenter = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z + 0.2f);
        Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);
        RaycastHit hit;
        if (Physics.BoxCast(boxCenter, halfExtents, controller.transform.forward, out hit, controller.transform.rotation, controller.AttackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (canAttack)
                {
                    canAttack = false;
                    controller.StartCoroutine(AttackCooldown());

                    // Damage target
                    hit.collider.GetComponentInChildren<StaminaBar>().TakeDamage(controller.AttackDamage);
                }
            }
        }

        // Go back to chasing
        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) >= 1.4f)
        {
            controller.ChangeState(controller.chaseState);
        }
    }

    public void OnHurt(StateController controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateController controller)
    {
        // "Must've been the wind"
    }
    
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
