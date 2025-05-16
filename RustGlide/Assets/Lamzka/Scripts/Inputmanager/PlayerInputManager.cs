using UnityEngine.InputSystem;

public class PlayerInputManager : InputSubject
{
    private XRPlayerInputActions controls;

    private void Awake()
    {
        controls = new XRPlayerInputActions();

        controls.Player.TriggerR.performed += OnTriggerR;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }


    public void OnTriggerR(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NotifyTriggerRValue(true);
        }
        else
        {
            NotifyTriggerRValue(false);
        }

    }

    public void OnTriggerL(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NotifyTriggerLValue(true);
        }
        else
        {
            NotifyTriggerLValue(false);
        }
    }
}
