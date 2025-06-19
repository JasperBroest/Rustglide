using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

public class AttackState : IState
{
    private StateController controller;
    private bool didAttack = false;
    private Coroutine attackCoroutine;

    public void OnEnter(StateController controller)
    {
        didAttack = false;
        this.controller = controller;

        controller.rb.linearVelocity = Vector3.zero;

        attackCoroutine = controller.StartCoroutine(AttackCooldown());
    }

    public void UpdateState(StateController controller)
    {
        // Go back to chasing
        if (didAttack)
        {
            controller.StopCoroutine(attackCoroutine);
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
        controller.graphics.transform.rotation = Quaternion.LookRotation(controller.Target.transform.position - controller.graphics.transform.position, Vector3.up
        );

        yield return new WaitForSeconds(0.5f);
        controller.rb.AddForce(Vector3.Normalize(controller.Target.transform.position - controller.transform.position) * 20f, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1.5f);
        didAttack = true;
    }

    public void FixedUpdateState(StateController controller)
    {
        return;
    }
}
