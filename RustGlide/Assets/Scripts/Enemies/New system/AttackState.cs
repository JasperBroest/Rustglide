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
        // "What was that!?"

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
        yield return new WaitForSeconds(1f);
        controller.rb.AddForce(controller.graphics.transform.forward * 15f, ForceMode.VelocityChange);
        yield return new WaitForSeconds(3f);
        didAttack = true;
    }

    public void FixedUpdateState(StateController controller)
    {
        return;
    }
}
