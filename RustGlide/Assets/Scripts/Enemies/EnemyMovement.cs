using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public bool foundPlayer = false;

    private NavMeshAgent agent;
    private Vector3 playerPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPlayer(Vector3 platerPos)
    {
        if (foundPlayer)
        {
            playerPosition = platerPos;
            agent.SetDestination(platerPos);
        }

        CheckForAttack();
    }
    private void CheckForAttack()
    {
        if (Vector3.Distance(playerPosition, transform.position) <= 2)
        {
            foundPlayer = false;
            agent.isStopped = true;
            Debug.Log("attack");
            StartCoroutine(wait());

        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        foundPlayer = true;
    }
}
