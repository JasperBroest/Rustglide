using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyList = new();
    public bool gameDone = false;

    public static EnemyManager Instance;

    public EnemyWave[] waves;
    private int waveCount = 0;

    public int killCount;

    [System.Serializable]
    public struct EnemyWave
    {
        public GameObject enemy;
        public int enemyAmount;
    }

    public void EnemiesClearedCheck()
    {
        if (enemyList.Count <= 0)
        {
            waveCount++;
            InitializeWave();
        }
    }

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

    private void Start()
    {
        InitializeWave();
    }

    private void InitializeWave()
    {
        if (waveCount < 3)
        {
            FindAnyObjectByType<XROrigin>().GetComponentInChildren<EnemiesLeft>().waveKillCount = 0;
            EnemySpawner.Instance.SpawnWave(waves[waveCount].enemy, waves[waveCount].enemyAmount);
            FindAnyObjectByType<XROrigin>().GetComponentInChildren<EnemiesLeft>().enemyCounter = enemyList.Count;
        }
        else
        {
            gameDone = true;

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(2);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(3);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(1);
            }
    
        }
    }
}

