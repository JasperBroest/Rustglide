using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

public class AttackStateTemp : IStateTemp
{
    private bool canAttack = true;

    public void OnEnter(StateControllerTemp controller)
    {
        // "What was that!?"
    }

    public void UpdateState(StateControllerTemp controller)
    {
        Vector3 boxCenter = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z + 0.2f);
        Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);
        RaycastHit hit;
        if (Physics.BoxCast(boxCenter, halfExtents, controller.transform.forward, out hit, controller.transform.rotation, controller.AttackRange))
        {
            //Check if has player layer
            if (hit.collider.gameObject.layer == 3)
            {
                if (canAttack)
                {
                    canAttack = false;
                    controller.StartCoroutine(AttackCooldown());

                    // Damage target
                    if (hit.collider.GetComponentInChildren<StaminaBar>() != null)
                    {
                        hit.collider.GetComponentInChildren<StaminaBar>().TakeDamage(controller.AttackDamage);
                        Debug.Log("Attacked");
                    }
                }
            }
        }

        // Go back to chasing
        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) >= controller.AttackRange)
        {
            controller.ChangeState(controller.chaseStateTemp);
        }
    }

    public void OnHurt(StateControllerTemp controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateControllerTemp controller)
    {
        // "Must've been the wind"
    }
    
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }

    public void FixedUpdateState(StateControllerTemp controller)
    {
        throw new System.NotImplementedException();
    }
}
