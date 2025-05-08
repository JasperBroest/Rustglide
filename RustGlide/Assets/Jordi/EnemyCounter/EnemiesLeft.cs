using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour
{
    private int enemyCounter;
    public TextMeshProUGUI text;

    private void Update()
    {
        enemyCounter = EnemyManager.Instance.enemyList.Count;
        text.text = "Enemies left: " + enemyCounter;
    }
}
