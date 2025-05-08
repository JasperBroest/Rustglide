using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float radius;

    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
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
                    enemyMovement.foundPlayer = true;
                }
            }
        }

        if (enemyMovement.foundPlayer)
        {
            enemyMovement.MoveToPlayer();
        }
    }
}
