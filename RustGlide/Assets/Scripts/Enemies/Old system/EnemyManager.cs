using System.Collections.Generic;
using System.Reflection;
using Unity.XR.CoreUtils;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyList = new();
    public bool gameDone = false;

    public static EnemyManager Instance;

    public EnemyWave[] waves;
    private int waveCount = 0;

    public int killCount;

    private List<ScriptableRendererFeature> features;
    public int XrayEnemyAmount;

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
        XrayEnemyAmount = AbilityManager.Instance.DefaultXrayEnemyAmount;
    }

    private void Start()
    {
        InitializeWave();
        UniversalRenderPipelineAsset urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;

        ScriptableRendererData[] rendererDataList = urpAsset.GetType().GetField("m_RendererDataList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(urpAsset) as ScriptableRendererData[];
        if (rendererDataList == null || rendererDataList.Length == 0)
        {
            Debug.LogError("No renderer data found.");
            return;
        }

        ScriptableRendererData rendererData = rendererDataList[0];
        features = rendererData.rendererFeatures;
        Debug.Log(features);
    }

    public void ToggleFeature(bool enabled)
    {
        foreach (var feature in features)
        {
            if (feature.name == "Xray")
            {
                feature.SetActive(enabled);
                Debug.Log($"{"Xray"} set to {enabled}");
                return;
            }
        }

        Debug.LogWarning($"Render feature Xray not found.");
    }

    private void Update()
    {
        if (enemyList.Count <= XrayEnemyAmount)
        {
            ToggleFeature(true);
        }
        else
        {
            ToggleFeature(false);
        }
    }

    private void InitializeWave()
    {
        if (waveCount < 3)
        {
            EnemySpawner.Instance.SpawnWave(waves[waveCount].enemy, waves[waveCount].enemyAmount);
            FindAnyObjectByType<EnemiesLeft>(FindObjectsInactive.Include).enemyCounter = enemyList.Count;
            FindAnyObjectByType<EnemiesLeft>(FindObjectsInactive.Include).waveKillCount = 0;
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

