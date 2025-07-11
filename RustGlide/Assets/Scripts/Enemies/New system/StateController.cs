using UnityEngine;

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
    public float MaxHealth;
    public float CurrentHealth;
    public float maxSpeed = 5f;
    public float minSpeed = 3f;

    [HideInInspector]
    public bool FoundTarget;
    public float DamageTaken;
    public GameObject Target;
    public Rigidbody rb;
    public Rigidbody TargetRigidbody;
    public MeshRenderer MeshRenderer;
    public LayerMask PlayerMask;
    public IState PreviousState;
    public GameObject graphics;
    public Material DefaultMat;
    public Material HitMat;

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

        //Debug.Log(currentState);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponentInChildren<StaminaBar>().TakeDamage(AttackDamage);
        }
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

