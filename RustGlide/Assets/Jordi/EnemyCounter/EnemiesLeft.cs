using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private int enemyCounter;
    public TextMeshProUGUI text;

    private void Start()
    {
        _enemyManager = FindFirstObjectByType<EnemyManager>();
    }

    private void Update()
    {
        enemyCounter = _enemyManager.enemyList.Count;
        text.text = "Enemies left: " + enemyCounter;
    }
}
