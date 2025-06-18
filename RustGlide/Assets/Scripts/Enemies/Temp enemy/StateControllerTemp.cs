using UnityEngine;
using UnityEngine.AI;

public class StateControllerTemp : MonoBehaviour
{
    // State Machine
    IStateTemp currentState;

    public IdleStateTemp idleStateTemp = new IdleStateTemp();
    public PatrolStateTemp patrolStateTemp = new PatrolStateTemp();
    public ChaseStateTemp chaseStateTemp = new ChaseStateTemp();
    public HurtStateTemp hurtStateTemp = new HurtStateTemp();
    public AttackStateTemp attackStateTemp = new AttackStateTemp();

    // Other variables
    [Header("Settings")]
    public float VisionRadius;
    public float AttackRange;
    public float AttackSpeed;
    public int AttackDamage;
    public int MaxHealth;
    public int CurrentHealth;

    [HideInInspector]
    public int DamageTaken;
    public bool FoundTarget;
    public GameObject Target;
    public Rigidbody rb;
    public Rigidbody TargetRigidbody;
    public MeshRenderer MeshRenderer;
    public NavMeshAgent Agent;
    public LayerMask PlayerMask;
    public Color OldColor;
    public IStateTemp PreviousState;

    private void Start()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();

        ChangeState(idleStateTemp);

        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        if (currentState != null)                                      
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IStateTemp newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        PreviousState = currentState;
        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IStateTemp
{
    public void OnEnter(StateControllerTemp controller);

    public void UpdateState(StateControllerTemp controller);

    public void OnHurt(StateControllerTemp controller);

    public void OnExit(StateControllerTemp controller);
}

