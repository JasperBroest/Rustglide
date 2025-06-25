using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour
{
    public int enemyCounter;
    public int waveKillCount;
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = EnemyManager.Instance.killCount + "of" + enemyCounter + Environment.NewLine + "Total kills: " + EnemyManager.Instance.killCount;
    }
}
