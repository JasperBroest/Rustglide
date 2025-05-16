using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // State Machine
    IState currentState;

    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public HurtState HurtState = new HurtState();
    public AttackState attackState = new AttackState();

    // Other variables
    public bool FoundTarget;
    public LayerMask PlayerMask;
    public float VisionRadius;
    public float AttackRange;
    public int AttackDamage;
    public GameObject Target;
    public NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ChangeState(idleState);
    }

    void Update()
    {
        if (currentState != null)                                      
        {
            currentState.UpdateState(this);
        }

        Debug.Log(currentState);

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
