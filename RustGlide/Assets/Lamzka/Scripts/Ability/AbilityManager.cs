using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [Header("this Instance")]
    public static AbilityManager Instance;

    [Header("===Weapon Stats===")]
    public float WeaponDamage;
    public float ShootingCooldown;

    [Space(20)]

    [Header("===ChosenMovement===")]
    public bool HasChosen;
    public string ChosenMovement;

    [Header("===PlayerStats===")]
    public float Stamina;

    [Header("Standard Movement")]
    public float StandardSpeed;

    [Header("Dash Movement")]
    public float DashSpeed;
    public float CooldownTime;

    [Header("Booster Movement")]
    public float BoosterSpeed;
    public float BoosterCooldown;

    [Header("Monkey Movement")]
    public float MonkeyJump;
    public float MonkeyMaxJumpSpeed;

    public bool TestBool;

    [SerializeField] GameObject gorilla;
    GameObject XrOrigin;


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

        XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;

        DontDestroyOnLoad(this);
    }

    private void OnEnable() // Or Awake, Start, etc.
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable() // To prevent memory leaks, unsubscribe when disabled
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        if (HasChosen)
        {
            if (ChosenMovement == "XrOrigin")
            {
                XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;
                XrOrigin.GetComponent<DashToDirection>().enabled = true;
            }
            else if (ChosenMovement == "gorilla")
            {
                GameObject Player = Instantiate(gorilla, FindFirstObjectByType<XROrigin>().transform);
                Player.transform.parent = null;
                XrOrigin.SetActive(false);
            }
        }
    }
}
