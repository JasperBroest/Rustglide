using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreStamina : MonoBehaviour
{
    public static StoreStamina instance;

    public float staminaLevelMultiplier = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnSceneLoaded()
    {
        staminaLevelMultiplier += 0.3f;
    }
}
