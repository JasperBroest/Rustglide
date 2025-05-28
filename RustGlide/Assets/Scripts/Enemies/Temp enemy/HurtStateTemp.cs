using System.Collections;
using UnityEngine;

// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// NEED TO TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public class HurtStateTemp : IStateTemp
{
    public void OnEnter(StateControllerTemp controller)
    {
        controller.CurrentHealth -= controller.DamageTaken;
    }

    public void UpdateState(StateControllerTemp controller)
    {
        // Spaghetti
        controller.OldColor = controller.MeshRenderer.material.color;
        controller.MeshRenderer.material.color = new Color(255, 255, 255, 255);
        controller.StartCoroutine(HitFlashEffect(controller.MeshRenderer, controller));
        //controller.ChangeState(controller.PreviousState);
    }

    public void OnHurt(StateControllerTemp controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateControllerTemp controller)
    {
        // "Must've been the wind"
    }

    private IEnumerator HitFlashEffect(MeshRenderer mesh, StateControllerTemp controller)
    {
        yield return new WaitForSeconds(0.1f);
        mesh.material.color = controller.OldColor;
    }
}
