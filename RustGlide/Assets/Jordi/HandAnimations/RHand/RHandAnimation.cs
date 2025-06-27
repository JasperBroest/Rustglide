using UnityEngine;

public class RHandAnimation : MonoBehaviour, IPlayerInput
{
    public Animator animator;

    private bool RGripPressed;
    private bool RTriggerPressed;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LeftGrip(bool LGrip)
    {
        
    }

    public void LeftTrigger(bool LState)
    {
        
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
        GetInput();
        if (RGripPressed)
        {
            animator.SetFloat("Grip", 1);
        }
        else
        {
            animator.SetFloat("Grip", 0);
        }

        if (RTriggerPressed)
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
