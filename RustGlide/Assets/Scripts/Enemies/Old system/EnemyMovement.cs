using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public bool foundPlayer = false;

    private EnemyAttack enemyAttack;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 playerPosition;

    public void MoveToPlayer()
    {
        Debug.Log("try move");
        if (!CheckForAttack())
        {
            Debug.Log("move");
            agent.SetDestination(playerPosition);
        }
    }

    private void Awake()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<XROrigin>().gameObject.transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        playerPosition = player.transform.position;
    }

    // Checks if enemy is in attack range
    private bool CheckForAttack()
    {
        if (Vector3.Distance(playerPosition, transform.position) <= 2)
        {
            agent.ResetPath();
            enemyAttack.Attack();

            return true;
        }

        else
        {
            return false;
        }
    }
}
