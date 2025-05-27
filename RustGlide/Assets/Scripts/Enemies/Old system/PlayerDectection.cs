using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerMask;

    private EnemyMovement enemyMovement;

    public GameObject DetectPotentialPlayer()
    {
        if (!enemyMovement.foundPlayer)
        {
            Collider[] sphere = Physics.OverlapSphere(transform.position, radius, playerMask);

            if (sphere.Length > 0)
            {
                DetectPlayerInSight(sphere[0].gameObject);
            }
        }

        return null;
    }

    private void DetectPlayerInSight(GameObject potentialPlayer)
    {
        // Calculate the direction vector correctly
        Vector3 direction = (potentialPlayer.transform.position - transform.position).normalized;

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 10f, playerMask))
        {
            enemyMovement.foundPlayer = true;
        }

        // Visualize the raycast
        Debug.DrawLine(transform.position, transform.position + direction * 10f, Color.red);


        if (enemyMovement.foundPlayer)
        {
            enemyMovement.MoveToPlayer();
        }
    }

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        DetectPotentialPlayer();
    }

}
