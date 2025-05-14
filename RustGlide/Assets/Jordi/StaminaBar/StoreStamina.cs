using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreStamina : MonoBehaviour
{
    public static StoreStamina instance;

    [HideInInspector] public float staminaLevelMultiplier = 1f;
    [SerializeField] private float multiplier = 0.3f;

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
        staminaLevelMultiplier += multiplier;
    }
}
