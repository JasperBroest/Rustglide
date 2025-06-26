using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [Header("this Instance")] public static AbilityManager Instance;
    
    [Header("===Weapon Stats===")] [Space(20)] 
    
    [Header("CurrentHandGun")]
    public float CurrentHandGunDamage;
    
    [Space(15)]
    
    [Header("CurrentSMG")] 
    public float CurrentSMGDamage;
    public float CurrentSMGShootingCooldown;
    
    [Space(15)]

    [Header("CurrentShotGun")] 
    public float CurrentShotgunDamage;
    public float CurrentShotgunCooldown;
    
    [Space(15)]
    [Header("===ChosenMovement===")]
    [HideInInspector]public bool HasChosen;

   [HideInInspector]public string ChosenMovement;

    [Header("===PlayerStats===")] 
    [Header("Stamina")]
    public float Stamina;
    
    [Header("Standard Movement")] 
    public float StandardSpeed;

    [Header("Dash Movement")]
    [HideInInspector]public float DashSpeed;
    [HideInInspector]public float CooldownTime;

    [Header("Booster Movement")] 
    public float BoosterSpeed;
    [HideInInspector]public float BoosterCooldown;
    
    [Space(25)]
    [Header("===Weapon Default Values===")]
    
    [Header("Handgun Default Values")] 
    public float DefaultHandGunDamage;
    [Space(15)]
    [Header("SMG Default Values")] 
    public float DefaultSMGDamage;
    public float DefaultSMGShootingCooldown;
    [Space(15)]
    [Header("Shotgun Default Values")]
    public float DefaultShotgunDamage;
    public float DefaultShotgunShootingCooldown;
    
    [Space(2)]
    
    [Header("===StatDefaults===")]
    
    [Header("Stamina Defaults")]
    public float DefaultStamina;
    
    [Header("Standard Movement defaults")] 
    public float DefaultStandardSpeed;
    
    
    [Header("Dash Movement defaults")]
    [HideInInspector] public float DefaultDashSpeed;
    [HideInInspector] public float DefaultCooldownTime;
    
    [Header("Booster Movement defaults  ")] 
    public float DefaultBoosterSpeed;
    [HideInInspector] float DefaultBoosterCooldown;

    [Header("Xray Defaults")]
    public int DefaultXrayEnemyAmount;



    [HideInInspector] [Header("Monkey Movement")] public float MonkeyJump;
    [HideInInspector] public float MonkeyMaxJumpSpeed;

    [Space(20)]
    public bool TestBool;

    [HideInInspector] [SerializeField] GameObject gorilla;
    [HideInInspector] GameObject XrOrigin;
    
 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ResetStats();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        if (HasChosen)
        {
            XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;
            if (ChosenMovement == "XrOrigin")
            {
                XrOrigin.GetComponent<DashToDirection>().enabled = true;
                XrOrigin.GetComponentInChildren<StaminaBar>().enabled = true;
            }
            else if (ChosenMovement == "gorilla")
            {
                GameObject Player = Instantiate(gorilla, FindFirstObjectByType<XROrigin>().transform);
                Player.transform.parent = null;
                Player.GetComponentInChildren<StaminaBar>().enabled = true;
                XrOrigin.SetActive(false);
            }
        }
    }

    public void ResetStats()
    {
        CurrentHandGunDamage = DefaultHandGunDamage;

        CurrentSMGDamage = DefaultSMGDamage;

        CurrentSMGShootingCooldown = DefaultSMGShootingCooldown;
        
        CurrentShotgunDamage = DefaultShotgunDamage;
        CurrentShotgunCooldown = DefaultShotgunShootingCooldown;

        Stamina =  DefaultStamina;

        StandardSpeed = DefaultStandardSpeed;
        
        DashSpeed = DefaultDashSpeed;
        CooldownTime = DefaultCooldownTime;

        BoosterSpeed = DefaultBoosterSpeed;
        BoosterCooldown =  DefaultBoosterCooldown;

    }
    
}