using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NG_UpgradeProgress : MonoBehaviour
{
    public float highestShards;
    public int highestHeartCrystals;
    public float tempShards;
    public float highestHealth;
    public float highestEnergy;
    public int highestLives = 3;

    //Unlock Bools**************************************************
    [SerializeField] private bool iceBall = false;
    [SerializeField] private bool rockBall = false;
    [SerializeField] private bool shotgun = false;
    [SerializeField] private bool explosion = false;
    [SerializeField] private bool grower = false;
    [SerializeField] private bool boomerang = false;
    [SerializeField] private bool healthpickupProj = false;
    [SerializeField] private bool dashUnlocked = false;
    [SerializeField] private bool forcePushUnlocked = false;
    //***************************************************************

    //Upgrade Bools**************************************************
    [SerializeField] private bool iceBallUpgrade = false;
    [SerializeField] private bool rockBallUpgrade = false;
    [SerializeField] private bool shotgunUpgrade = false;
    [SerializeField] private bool explosionUpgrade = false;
    [SerializeField] private bool growerUpgrade = false;
    [SerializeField] private bool boomerangUpgrade = false;
    [SerializeField] private bool healthpickupProjUpgrade = false;
    [SerializeField] private bool dashUpgrade = false;
    [SerializeField] private bool forcePushUpgrade = false;
    //***************************************************************

    private NG_StatManager playerStats;
    [SerializeField] private NG_UpgradeMenu upgradeMenu;
    [SerializeField] private ScoreManager score;
    [SerializeField] private NG_CircleMenu circleMenu;

    public static NG_UpgradeProgress UpgradeProgress;

    //Unlock Get/Set***************************************************
    public bool IceBall
    { get { return iceBall; } set { iceBall = value; } }
    public bool RockBall
    { get { return rockBall; } set { rockBall = value; } }
    public bool Shotgun
    { get { return shotgun; } set { shotgun = value; } }
    public bool Explosion
    { get { return explosion; } set { explosion = value; } }
    public bool Grower
    { get { return grower; } set { grower = value; } }
    public bool Boomerang
    { get { return boomerang; } set { boomerang = value; } }
    public bool HealthPickupProj
    { get { return healthpickupProj; } set { healthpickupProj = value; } }
    public bool DashUnlocked
    { get { return dashUnlocked; } set { dashUnlocked = value; } }
    public bool ForcePushUnlocked
    { get { return forcePushUnlocked; } set { forcePushUnlocked = value; } }
    //*****************************************************************

    //Upgrade Get/Set****************************************************
    public bool IceBallUpgrade
    { get { return iceBallUpgrade; } set { iceBallUpgrade = value; } }
    public bool RockBallUpgrade
    { get { return rockBallUpgrade; } set { rockBallUpgrade = value; } }
    public bool ShotgunUpgrade
    { get { return shotgunUpgrade; } set { shotgunUpgrade = value; } }
    public bool ExplosionUpgrade
    { get { return explosionUpgrade; } set { explosionUpgrade = value; } }
    public bool GrowerUpgrade
    { get { return growerUpgrade; } set { growerUpgrade = value; } }
    public bool BoomerangUpgrade
    { get { return boomerangUpgrade; } set { boomerangUpgrade = value; } }
    public bool HealthPickupProjUpgrade
    { get { return healthpickupProjUpgrade; } set { healthpickupProjUpgrade = value; } }
    public bool DashUpgrade
    { get { return dashUpgrade; } set { dashUpgrade = value; } }
    public bool ForcePushUpgrade
    { get { return forcePushUpgrade; } set { forcePushUpgrade = value; } }
    //*****************************************************************
    
    private void Awake()
    {
        if (UpgradeProgress)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            UpgradeProgress = this;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
            score = FindObjectOfType<ScoreManager>();
    }

    private void Start()
    {
        playerStats = FindObjectOfType<NG_StatManager>();
        upgradeMenu = FindObjectOfType<NG_UpgradeMenu>();
        circleMenu = FindObjectOfType<NG_CircleMenu>();
        score = FindObjectOfType<ScoreManager>();
        if (score == null)
            Debug.Log("ScoreManager didn't load between scenes.");

        highestShards = score.shardCount;
        highestHealth = playerStats.Health.MaxVal;
        highestEnergy = playerStats.Energy.MaxVal;
        highestLives = playerStats.MaxLives;
    }

    private void Update()
    {
        //highestShards = score.shardCount;
        //tempShards = highestShards;
        if (score == null)
        {
            Debug.Log("ScoreManager didn't load between scenes.");
            //highestShards = tempShards;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
            highestShards += 8;
            
        upgradeMenu = FindObjectOfType<NG_UpgradeMenu>();
        circleMenu = FindObjectOfType<NG_CircleMenu>();

        score.shardCount = highestShards;
    }
}
