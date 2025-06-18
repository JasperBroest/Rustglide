using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;

    public static EnemySpawner Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnWave(GameObject enemy,int spawnAmount)
    {
        foreach(Transform spawn in spawnpoints)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject clone = Instantiate(enemy, new Vector3(spawn.position.x + Random.Range(-2, 2), spawn.position.y, spawn.position.z + Random.Range(-2, 2)), Quaternion.identity);
                EnemyManager.Instance.enemyList.Add(clone);
            }
        }
    }


}
