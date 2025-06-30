using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyList = new();

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
    }

    private void Start()
    {
        InitializeWave();
        UniversalRenderPipelineAsset urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;

        ScriptableRendererData[] rendererDataList = urpAsset.GetType().GetField("m_RendererDataList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(urpAsset) as ScriptableRendererData[];
        if (rendererDataList == null || rendererDataList.Length == 0)
        {
            return;
        }

        ScriptableRendererData rendererData = rendererDataList[0];
        features = rendererData.rendererFeatures;
        XrayEnemyAmount = AbilityManager.Instance.DefaultXrayEnemyAmount;
    }

    public void ToggleFeature(bool enabled)
    {
        foreach (var feature in features)
        {
            if (feature.name == "Xray")
            {
                feature.SetActive(enabled);
                return;
            }
        }
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
        if (waveCount < waves.Length)
        {
            // Spawn new wave
            EnemySpawner.Instance.SpawnWave(waves[waveCount].enemy, waves[waveCount].enemyAmount);

            // Show how many enemies left
            var enemiesLeft = FindAnyObjectByType<EnemiesLeft>(FindObjectsInactive.Include);
            if (enemiesLeft != null)
            {
                enemiesLeft.enemyCounter = enemyList.Count;
                enemiesLeft.waveKillCount = 0;
            }
        }
        else
        {
            LevelManager.Instance.LoadNextLevel();        
        }
    }
}