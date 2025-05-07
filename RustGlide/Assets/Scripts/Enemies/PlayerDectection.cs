using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float radius;

    private EnemyMovement enemyMovement;
    private GameObject player;
    private Vector3 target;

    private void Awake()
    {
        enemyMovement = FindFirstObjectByType<EnemyMovement>();
    }

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (!enemyMovement.foundPlayer)
        {
            Collider[] sphere = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in sphere)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    player = hit.gameObject;
                    enemyMovement.foundPlayer = true;
                }
            }
        }

        if (enemyMovement.foundPlayer)
        {
            target = player.transform.position;
            enemyMovement.MoveToPlayer(target);
            Debug.Log("Found player");
        }
    }
}
