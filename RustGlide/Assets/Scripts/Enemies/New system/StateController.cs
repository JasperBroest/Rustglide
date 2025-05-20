using UnityEditorInternal;
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
    public bool FoundTarget;
    public LayerMask PlayerMask;
    public float VisionRadius;
    public float AttackRange;
    public int AttackDamage;
    public GameObject Target;
    public NavMeshAgent agent;
    public int CurrentHealth;
    public int MaxHealth;
    public int DamageTaken;
    public IState PreviousState;
    public MeshRenderer meshRenderer;
    public Color oldColor;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

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

        // If in attackrange stop moving
        if (Vector3.Distance(transform.position, Target.transform.position) <= 1.4f)
        {
            agent.ResetPath();
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

    public void OnHurt(StateController controller);

    public void OnExit(StateController controller);
}
