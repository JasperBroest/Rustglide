using UnityEngine;

public class HandAnimation : MonoBehaviour, IPlayerInput
{
    Animator animator;

    private bool LGripPressed;
    private bool LTriggerPressed;
    private bool RGripPressed;
    private bool RTriggerPressed;

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
        Debug.Log(LTriggerPressed);
    }

    public void RightGrip(bool Rgrip)
    {
        RGripPressed = Rgrip;
    }

    public void RightTrigger(bool RState)
    {
        RTriggerPressed = RState;
    }

    private void Update()
    {
        if(LGripPressed)
        {
            animator.SetFloat("Grip", 1);
            Debug.Log(animator.GetFloat("Grip"));
        }
        else
        {
            animator.SetFloat("Grip", 0);
            Debug.Log(animator.GetFloat("Grip"));
        }

        if (LTriggerPressed)
        {
            animator.SetFloat("Trigger", 1);
            Debug.Log(animator.GetFloat("Trigger"));
        }
        else
        {
            animator.SetFloat("Trigger", 0);
            Debug.Log(animator.GetFloat("Trigger"));
        }
    }
}
