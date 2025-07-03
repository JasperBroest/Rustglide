using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;

    public static EnemySpawner Instance;

    [SerializeField]private GameObject[] EnemyTypes;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnWave(int spawnAmount)
    {
        RefreshSpawnpoints();
        foreach(Transform spawn in spawnpoints)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject clone = Instantiate(EnemyTypes[Random.Range(0, EnemyTypes.Length)], new Vector3(spawn.position.x + Random.Range(-5, 5), spawn.position.y, spawn.position.z + Random.Range(-5, 5)), Quaternion.identity);
                EnemyManager.Instance.enemyList.Add(clone);
            }
        }
    }

    public void RefreshSpawnpoints()
    {
        var allActiveTransforms = GetComponentsInChildren<Transform>();
        spawnpoints = new Transform[allActiveTransforms.Length - 1];
        for (int i = 1; i < allActiveTransforms.Length; i++)
        {
            spawnpoints[i - 1] = allActiveTransforms[i];
        }
    }
}
