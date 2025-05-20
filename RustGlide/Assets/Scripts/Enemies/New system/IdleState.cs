using System.Collections;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(StateController controller)
    {
        // Play idle animation

    }

    public void UpdateState(StateController controller)
    {
        #region Player Detection
        Collider[] sphere = Physics.OverlapSphere(controller.transform.position, controller.VisionRadius, controller.PlayerMask);

        if (sphere.Length > 0)
        {
            GameObject potentialTarget = sphere[0].gameObject;

            // Calculate the direction vector
            Vector3 direction = (potentialTarget.transform.position - controller.transform.position).normalized;

            // Perform raycast
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, direction, out hit, controller.VisionRadius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    controller.FoundTarget = true;
                    controller.Target = hit.collider.gameObject;
                }               
            }

            // Visualize raycast
            Debug.DrawLine(controller.transform.position, controller.transform.position + direction * 10f, Color.red);
        }
        #endregion

        if (controller.FoundTarget)
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

    private IEnumerator PatrolCooldown()
    {
        yield return new WaitForSeconds(5f);
    }
}
