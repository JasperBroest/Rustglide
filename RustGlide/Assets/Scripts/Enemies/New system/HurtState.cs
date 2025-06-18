using System.Collections;
using UnityEngine;

public class HurtState : IState
{
    public void OnEnter(StateController controller)
    {
        controller.CurrentHealth -= controller.DamageTaken;
        if(controller.CurrentHealth < 0 )
        {
            EnemyManager.Instance.enemyList.Remove(controller.gameObject);
            EnemyManager.Instance.EnemiesClearedCheck();
            GameObject.Destroy(controller.gameObject);
        }
        Debug.Log(controller.MeshRenderer.gameObject.name);
        controller.OldColor = controller.MeshRenderer.material.color;
        controller.MeshRenderer.material.color = new Color(255, 255, 255, 255);
        controller.StartCoroutine(HitFlashEffect(controller.MeshRenderer, controller));
    }

    public void UpdateState(StateController controller)
    {
        if (controller.PreviousState != controller.hurtState)
        {
            controller.ChangeState(controller.PreviousState);
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

    private IEnumerator HitFlashEffect(MeshRenderer mesh, StateController controller)
    {
        yield return new WaitForSeconds(0.1f);
        mesh.material.color = controller.OldColor;
    }

    public void FixedUpdateState(StateController controller)
    {
        
    }
}
