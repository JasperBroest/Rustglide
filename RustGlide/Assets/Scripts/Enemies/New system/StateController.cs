using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // State Machine
    IState currentState;

    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public HurtState hurtState = new HurtState();
    public AttackState attackState = new AttackState();

    // Other variables
    [Header("Settings")]
    public float VisionRadius;
    public float AttackRange;
    public float AttackSpeed;
    public int AttackDamage;
    public int MaxHealth;
    public int CurrentHealth;
    public float maxSpeed = 5f;
    public float minSpeed = 3f;

    [HideInInspector]
    public int DamageTaken;
    public bool FoundTarget;
    public GameObject Target;
    public Rigidbody rb;
    public Rigidbody TargetRigidbody;
    public MeshRenderer MeshRenderer;
    public LayerMask PlayerMask;
    public Color OldColor;
    public IState PreviousState;
    public NavMeshAgent agent;
    public GameObject graphics;

    private void Start()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        ChangeState(idleState);

        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        if (currentState != null)                                      
        {
            currentState.UpdateState(this);
        }

        Debug.Log(currentState);
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdateState(this);
        }
    }

    public void ChangeState(IState newState)
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

public interface IState
{
    public void OnEnter(StateController controller);

    public void UpdateState(StateController controller);

    public void FixedUpdateState(StateController controller);

    public void OnHurt(StateController controller);

    public void OnExit(StateController controller);
}

