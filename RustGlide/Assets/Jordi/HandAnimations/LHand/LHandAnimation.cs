using UnityEngine;

public class LHandAnimation : MonoBehaviour, IPlayerInput
{
    public Animator animator;

    private bool LGripPressed;
    private bool LTriggerPressed;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LeftGrip(bool LGrip)
    {
        LGripPressed = LGrip;
    }

    public void LeftTrigger(bool LState)
    {
        LTriggerPressed = LState;
    }

    public void RightGrip(bool Rgrip)
    {
        
    }

    public void RightTrigger(bool RState)
    {
        
    }

    private void Update()
    {
        GetInput();
        if (LGripPressed)
        {
            animator.SetFloat("Grip", 1);
        }
        else
        {
            animator.SetFloat("Grip", 0);
        }

        if (LTriggerPressed)
        {
            animator.SetFloat("Trigger", 1);
        }
        else
        {
            animator.SetFloat("Trigger", 0);
        }
    }

    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}
