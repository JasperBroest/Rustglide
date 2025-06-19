using System.Collections;
using UnityEngine;

public class HurtState : IState
{
    private Color oldColor;
    public void OnEnter(StateController controller)
    {
        controller.CurrentHealth -= controller.DamageTaken;
        if(controller.CurrentHealth < 0 )
        {
            EnemyManager.Instance.enemyList.Remove(controller.gameObject);
            EnemyManager.Instance.EnemiesClearedCheck();
            GameObject.Destroy(controller.gameObject);
        }
        controller.GetComponentInChildren<Renderer>().material = controller.HitMat;
        controller.StartCoroutine(HitFlashEffect(controller));
    }

    public void UpdateState(StateController controller)
    {
        
    }

    public void OnHurt(StateController controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateController controller)
    {
        // "Must've been the wind"
    }

    private IEnumerator HitFlashEffect(StateController controller)
    {
        yield return new WaitForSeconds(0.1f);

        controller.GetComponentInChildren<Renderer>().material = controller.DefaultMat;

        if (controller.PreviousState != controller.hurtState)
        {
            controller.ChangeState(controller.PreviousState);
        }
    }

    public void FixedUpdateState(StateController controller)
    {
        
    }
}
