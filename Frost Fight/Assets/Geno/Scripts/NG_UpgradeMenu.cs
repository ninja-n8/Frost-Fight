using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NG_UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text energyText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text iceballText;
    [SerializeField] private Text rockballText;
    [SerializeField] private Text shotgunText;
    [SerializeField] private Text explosionText;
    [SerializeField] private Text growerText;
    [SerializeField] private Text boomerangText;
    [SerializeField] private Text healthpickupProjText;
    //[SerializeField] private Text dashText;
    //[SerializeField] private Text forcePushText;
    [SerializeField] private Text shardUpgradeText;
    [SerializeField] private Text heartUpgradeText;

    [SerializeField] private float healthMultiplier = 1.25f;
    [SerializeField] private float healthAdditive = 1;
    [SerializeField] private float energyMultiplier = 1.3f;
    [SerializeField] private float energyAdditive = 1;

    [SerializeField] private float freezeTimeMultiplier = 1.25f;
    [SerializeField] private float freezeTimeAdditive = 0.2f;
    [SerializeField] private float rockDamageMultiplier = 1.25f;
    [SerializeField] private float rockDamageAdditive = 1;

    //[SerializeField] private bool iceBall = false;
    //[SerializeField] private bool rockBall = false;

    [SerializeField] private float upgradeCost = 4.0f;
    [SerializeField] private float iceUpgradeCost = 4.0f;
    [SerializeField] private float rockUpgradeCost = 4.0f;
    [SerializeField] private float boomerangUpgradeCost = 4.0f;
    [SerializeField] private float healthPickupUpgradeCost = 4.0f;
    [SerializeField] private int heartCost = 1;

    private NG_Player player;
    private NG_StatManager playerStats;
    private NG_Weapon playerWeapon;
    private ScoreManager scoreManager;
    [SerializeField] private NG_UpgradeProgress upgradeProgress;
    [SerializeField] private NG_CircleMenu circleMenu;

    /*public bool IceBall
    { get { return iceBall; } set { iceBall = value; } }
    public bool RockBall
    { get { return rockBall; } set { rockBall = value; } }*/

    private void OnLevelWasLoaded(int level)
    {
        //if(level == 2)
            //UpdateValues();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<NG_Player>();
        playerStats = FindObjectOfType<NG_StatManager>();
        playerWeapon = FindObjectOfType<NG_Weapon>();
        scoreManager = FindObjectOfType<ScoreManager>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        circleMenu = FindObjectOfType<NG_CircleMenu>();
        UpdateValues();
    }

    // Use this for initialization
    void Start()
    {
        //UpdateValues();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpgradeHealth();
        //UpgradeEnergy();
        UpdateValues();

        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        heartUpgradeText.text = "Heart Crystals: " + upgradeProgress.highestHeartCrystals;
    }

    void UpdateValues()
    {
        healthText.text = "Health: " + Mathf.Floor(playerStats.Health.MaxVal).ToString();
        energyText.text = "Energy: " + Mathf.Floor(playerStats.Energy.MaxVal).ToString();
        livesText.text = "Lives: " + playerStats.MaxLives;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        heartUpgradeText.text = "Heart Crystals: " + upgradeProgress.highestHeartCrystals;

        if (upgradeProgress.IceBall)
            iceballText.text = "Freeze Duration +";
        else
        {
            iceballText.text = "Locked";
            Transform iceButton = iceballText.gameObject.transform.parent;
            iceButton.GetChild(1).GetComponent<Text>().text = "Complete World 2 to Unlock";
        }

        if (upgradeProgress.RockBall)
            rockballText.text = "Rockball Damage +";
        else
        {
            rockballText.text = "Locked";
            Transform rockButton = rockballText.gameObject.transform.parent;
            rockButton.GetChild(1).GetComponent<Text>().text = "Complete World 1 to Unlock";
        }

        if (upgradeProgress.Shotgun)
            shotgunText.text = "Select ammo type with R";
        else
            shotgunText.text = "Locked";

        if (upgradeProgress.Explosion)
            explosionText.text = "Select ammo type with R";
        else
            explosionText.text = "Locked";

        if (upgradeProgress.Grower)
            growerText.text = "Select ammo type with R";
        else
            growerText.text = "Locked";

        if (upgradeProgress.Boomerang)
            boomerangText.text = "Range +";
        else
        {
            boomerangText.text = "Locked";
            Transform boomerangButton = boomerangText.gameObject.transform.parent;
            boomerangButton.GetChild(1).GetComponent<Text>().text = "Complete World 3 to Unlock";
        }

        if (upgradeProgress.HealthPickupProj)
            healthpickupProjText.text = "Health Regen +";
        else
        {
            healthpickupProjText.text = "Locked";
            Transform healthProjButton = healthpickupProjText.gameObject.transform.parent;
            healthProjButton.GetChild(1).GetComponent<Text>().text = "Complete World 3 to Unlock";
        }

        /*if (upgradeProgress.DashUnlocked)
            dashText.text = "Press Shift to dash";
        if (upgradeProgress.ForcePushUnlocked)
            forcePushText.text = "Press Q to unleash a blast";*/

        if (upgradeProgress.IceBallUpgrade)
        {
            iceballText.fontSize = 14;
            iceballText.text = "Freeze Duration: " + Mathf.Round(playerStats.FreezeTime * 100) / 100 + " seconds.";
            Transform iceButton = iceballText.gameObject.transform.parent;
            iceButton.GetChild(1).GetComponent<Text>().text = "Increase Freeze Duration (Cost " + iceUpgradeCost + " shards)";
        }

        if (upgradeProgress.RockBallUpgrade)
        {
            rockballText.fontSize = 16;
            rockballText.text = "Rockball Damage X " + Mathf.Round(playerWeapon.RockDamageMultiplier * 10) / 10;
            Transform rockButton = rockballText.gameObject.transform.parent;
            rockButton.GetChild(1).GetComponent<Text>().text = "Increases Rock Damage (Cost " + rockUpgradeCost + " shards)";
        }

        //upgradeProgress.highestShards = scoreManager.shardCount;
        upgradeProgress.highestHealth = playerStats.Health.MaxVal;
        upgradeProgress.highestEnergy = playerStats.Energy.MaxVal;
        upgradeProgress.highestLives = playerStats.MaxLives;
    }

    public void UpgradeHealth()
    {
        if(upgradeProgress.highestShards < upgradeCost)
        {
            return;
        }

        healthAdditive *= healthMultiplier;
        playerStats.Health.MaxVal += healthAdditive;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        playerStats.Health.CurrentVal = playerStats.Health.MaxVal;
        UpdateValues();
    }

    public void UpgradeEnergy()
    {
        if (upgradeProgress.highestShards < upgradeCost)
        {
            return;
        }

        energyAdditive *= energyMultiplier;
        playerStats.Energy.MaxVal += energyAdditive;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        UpdateValues();
    }

    public void UpgradeLives()
    {
        if (upgradeProgress.highestHeartCrystals < heartCost)
        {
            return;
        }

        playerStats.MaxLives++;
        upgradeProgress.highestHeartCrystals -= heartCost;
        heartUpgradeText.text = "Heart Crystals: " + upgradeProgress.highestHeartCrystals;
        playerStats.CurLives = playerStats.MaxLives;
        UpdateValues();
    }

    public void ActivateIceBall()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.IceBall == true)
        {
            return;
        }

        upgradeProgress.IceBall = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[1].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateRockBall()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.RockBall == true)
        {
            return;
        }

        upgradeProgress.RockBall = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[2].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateShotgun()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.Shotgun == true)
        {
            return;
        }

        upgradeProgress.Shotgun = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[6].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateExplosion()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.Explosion == true)
        {
            return;
        }

        upgradeProgress.Explosion = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[4].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateGrower()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.Grower == true)
        {
            return;
        }

        upgradeProgress.Grower = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[5].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateBoomerang()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.Boomerang == true)
        {
            return;
        }

        upgradeProgress.Boomerang = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[3].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateHealthPickupProj()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.HealthPickupProj == true)
        {
            return;
        }

        upgradeProgress.HealthPickupProj = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        circleMenu.buttons[7].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        UpdateValues();
    }

    public void ActivateDash()
    {
        if (upgradeProgress.highestShards < upgradeCost || upgradeProgress.DashUnlocked == true)
        {
            return;
        }

        upgradeProgress.DashUnlocked = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        UpdateValues();
    }

    public void ActivateForcePush()
    {
        if(upgradeProgress.highestShards < upgradeCost || upgradeProgress.ForcePushUnlocked == true)
        {
            return;
        }

        upgradeProgress.ForcePushUnlocked = true;
        upgradeProgress.highestShards -= upgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        UpdateValues();
    }

    public void UpgradeIceBall()
    {
        if (upgradeProgress.highestShards < iceUpgradeCost /*|| upgradeProgress.IceBallUpgrade == true*/ || upgradeProgress.IceBall == false)
        {
            return;
        }

        upgradeProgress.IceBallUpgrade = true;
        upgradeProgress.highestShards -= iceUpgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        iceUpgradeCost *= 4;
        //player.iceBall = player.iceLaunchBall;
        freezeTimeAdditive *= freezeTimeMultiplier;
        playerStats.FreezeTime += freezeTimeAdditive;
        UpdateValues();
    }

    public void UpgradeRockBall()
    {
        if (upgradeProgress.highestShards < rockUpgradeCost /*|| upgradeProgress.RockBallUpgrade == true*/ || upgradeProgress.RockBall == false)
            return;

        upgradeProgress.RockBallUpgrade = true;
        upgradeProgress.highestShards -= rockUpgradeCost;
        shardUpgradeText.text = "Shards: " + upgradeProgress.highestShards;
        rockUpgradeCost *= 4;
        rockDamageAdditive *= rockDamageMultiplier;
        playerWeapon.RockDamageMultiplier += rockDamageAdditive;
        UpdateValues();
    }
}
