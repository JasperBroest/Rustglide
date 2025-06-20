using System.Collections;
using UnityEngine;

public class IdleState : IState
{
    private bool extraVision = true;

    public void OnEnter(StateController controller)
    {
        // Quick fix ehheheh
        controller.Target = GameObject.Find("Player");
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
                if (hit.collider.gameObject.layer == 3)
                {
                    controller.FoundTarget = true;
                    controller.Target = hit.collider.gameObject;
                    controller.TargetRigidbody = controller.Target.GetComponent<Rigidbody>();
                }               
            }

            // Visualize raycast
            //Debug.DrawLine(controller.transform.position, controller.transform.position + direction * 10f, Color.red);
        }
        #endregion

        // Random extra vision checks
        if (extraVision)
        {
            extraVision = false;
            controller.StartCoroutine(ExtraVisionCheck(controller));
        }

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

    private IEnumerator ExtraVisionCheck(StateController controller)
    {
        yield return new WaitForSeconds(10f);
        ExtraVision(controller);
    }

    public void FixedUpdateState(StateController controller)
    {
        return;
    }

    private void ExtraVision(StateController controller)
    {
        RaycastHit hit;
        if (Physics.Raycast(controller.transform.position, Vector3.Normalize(controller.Target.transform.position - controller.transform.position), out hit, 200))
        {
            if (hit.collider.gameObject.layer == 3)
            {
                controller.Target = hit.collider.gameObject;
                controller.TargetRigidbody= controller.Target.GetComponent<Rigidbody>();
                controller.FoundTarget = true;
            }
        }
        extraVision = true;
    }
}
