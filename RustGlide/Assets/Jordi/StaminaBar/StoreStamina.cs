using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreStamina : MonoBehaviour
{
    public static StoreStamina instance;

    private StaminaBar StaminaBar;

    public float staminaLevelMultiplier = 1f;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StaminaBar = FindFirstObjectByType<StaminaBar>();
    }

    public void OnSceneLoaded()
    {
        staminaLevelMultiplier += 0.2f;
    }
}
