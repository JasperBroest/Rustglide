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

        controls.Player.GripR.performed += OnGripR;
        controls.Player.GripR.canceled += OnGripR;

        controls.Player.GripL.performed += OnGripL;
        controls.Player.GripR.canceled += OnGripL;

        controls.Player.Menu.canceled += OnMenu;
        
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
        else if (context.canceled)
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
        else if (context.canceled)
        {
            NotifyTriggerLValue(false);
        }
    }

    public void OnGripR(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NotifyGripRValue(true);
        }
        else if (context.canceled)
        {
            NotifyGripRValue(false);
        }
    }

    public void OnGripL(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NotifyGripLValue(true);
        }
        else if (context.canceled)
        {
            NotifyGripLValue(false);
        }
    }

    private void OnMenu(InputAction.CallbackContext context)
    {
        Canvas menu = FindFirstObjectByType<MainMenuManager>().GetComponentInParent<Canvas>();
        if (menu != null)
        {
            menu.enabled = !menu.enabled;
        }
    }
}
