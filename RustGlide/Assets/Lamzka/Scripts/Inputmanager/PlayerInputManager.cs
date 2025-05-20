using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : InputSubject
{
    private XRPlayerInputActions controls;

    private void Awake()
    {
        controls = new XRPlayerInputActions();

        controls.Player.TriggerR.performed += OnTriggerR;
        controls.Player.TriggerR.canceled += OnTriggerR;

        controls.Player.TriggerL.performed += OnTriggerL;
        controls.Player.TriggerL.canceled += OnTriggerL;
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
            Debug.Log("true");

        }
        else if (context.canceled)
        {
            NotifyTriggerRValue(false);
            Debug.Log("false");
        }
    }

    public void OnTriggerL(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NotifyTriggerLValue(true);
        }
        else if (context.canceled)
        {
            NotifyTriggerLValue(false);
        }
    }
}
