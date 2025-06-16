using System.Collections;
using UnityEngine;

// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public class HurtState : IState
{
    public void OnEnter(StateController controller)
    {
        controller.CurrentHealth -= controller.DamageTaken;
    }

    public void UpdateState(StateController controller)
    {
        // Spaghetti
        controller.OldColor = controller.MeshRenderer.material.color;
        controller.MeshRenderer.material.color = new Color(255, 255, 255, 255);
        controller.StartCoroutine(HitFlashEffect(controller.MeshRenderer, controller));
        controller.ChangeState(controller.PreviousState);
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
