using System.Collections;
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
        agent.SetDestination(playerPosition);

        CheckForAttack();
    }

    private void Awake()
    {
        enemyAttack = FindFirstObjectByType<EnemyAttack>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        playerPosition = player.transform.position;
    }

    // Checks if enemy is in attack range
    private void CheckForAttack()
    {
        if (Vector3.Distance(playerPosition, transform.position) <= 2)
        {
            Debug.Log("attack");
            foundPlayer = false;
            agent.isStopped = true;

            enemyAttack.Attack();
            StartCoroutine(wait()); // Temp
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2.5f);
        foundPlayer = true;
        agent.isStopped = false;
    }
}
