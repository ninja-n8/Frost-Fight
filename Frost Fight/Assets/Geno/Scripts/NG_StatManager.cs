using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NG_StatManager : MonoBehaviour
{


    [SerializeField] GameObject EndMenu;

    private CheckpointManager checkPoint;
    private ScoreManager Score;
    private NG_EnemyStatManager enemyStats;
    [SerializeField] private NG_UpgradeProgress upgradeProgress;
    private SoundManager audio;
    private NG_CharacterController2D controller2D;

    [SerializeField] Stat health;
    [SerializeField] Stat energy;

    [SerializeField] int maxLives = 3;
    [SerializeField] int curLives;
    [SerializeField] Image[] hearts;
    [SerializeField] GameObject livesCounter;

    [SerializeField] float freezeTime;

    [SerializeField] private bool UpgradeRun = false;
    [SerializeField] private bool forceFieldOn = false;
    [SerializeField] private bool forceFieldCreated = false;
    [SerializeField] private GameObject forceFieldEffect;

    public Stat Health
    { get { return health; } set { health = value; } }
    public Stat Energy
    { get { return energy; } set { energy = value; } }

    public int MaxLives
    { get { return maxLives; } set { maxLives = value; } }
    public int CurLives
    { get { return curLives; } set { curLives = value; } }

    public float FreezeTime
    { get { return freezeTime; } set { freezeTime = value; } }

    public bool ForceFieldOn
    { get { return forceFieldOn; } set { forceFieldOn = value; } }


    private void Awake()
    {
        health.Initialize();
        energy.Initialize();
        HeartsFill();

        //curLives = 3;
    }

    // Use this for initialization
    void Start()
    {
        checkPoint = FindObjectOfType<CheckpointManager>();
        Score = FindObjectOfType<ScoreManager>();
        enemyStats = FindObjectOfType<NG_EnemyStatManager>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        audio = FindObjectOfType<SoundManager>();
        controller2D = GetComponent<NG_CharacterController2D>();

        LoadPlayerUpgrades();
        health.CurrentVal = health.MaxVal;
        maxLives = upgradeProgress.highestLives;
        curLives = maxLives;

        //health.MaxVal = 3;
        //energy.MaxVal = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.CurrentVal == 0)
            Death();
        GameOver();
        //Upgrade();
        ForceField();
        LivesCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //adding health when touching healthpickup
        if (other.gameObject.tag == "HealthPickup")
        {
            Destroy(other.gameObject);
            health.CurrentVal += 5;
        }
    }

    public void Damage()
    {
        if (!forceFieldOn)
        {
            health.CurrentVal -= enemyStats.EnemyDamage;
            audio.hurt();
            Debug.Log("hurt");
        }

        else
        {
            Destroy(this.transform.Find("ForceFieldObject(Clone)").gameObject);
            forceFieldOn = false;
        }
    }

    public void FallPillarDamage()
    {
        StartCoroutine(FallPillarCoroutine());
    }

    public IEnumerator FallPillarCoroutine()
    {
        //Debug.Log("coroutine");
        if (controller2D.ContactTags.Contains("MovingPlatform") && controller2D.ContactTags.Contains("Glass"))
            Health.CurrentVal = 0;
        yield return new WaitForSeconds(0.5f);
        if (controller2D.ContactTags.Contains("Ground") && controller2D.ContactTags.Contains("FallPillar"))
            Health.CurrentVal = 0;

    }

    public void Death()
    {
        checkPoint.PlayerSpawn();
    }

    public void LivesCounter()
    {
        if (curLives == maxLives)
            return;
        if (curLives < maxLives)
        {
            hearts[curLives].enabled = false;
        }
    }

    public void HeartsFill()
    {
        livesCounter = GameObject.Find("LivesCounter");

        hearts[0] = livesCounter.transform.GetChild(0).GetComponent<Image>();
        hearts[1] = livesCounter.transform.GetChild(1).GetComponent<Image>();
        hearts[2] = livesCounter.transform.GetChild(2).GetComponent<Image>();
        hearts[3] = livesCounter.transform.GetChild(3).GetComponent<Image>();
        hearts[4] = livesCounter.transform.GetChild(4).GetComponent<Image>();
        hearts[5] = livesCounter.transform.GetChild(5).GetComponent<Image>();
        hearts[6] = livesCounter.transform.GetChild(6).GetComponent<Image>();
        hearts[7] = livesCounter.transform.GetChild(7).GetComponent<Image>();
        hearts[8] = livesCounter.transform.GetChild(8).GetComponent<Image>();
        hearts[9] = livesCounter.transform.GetChild(9).GetComponent<Image>();
    }

    void Upgrade()
    {
        if (!UpgradeRun && Score.scoreCount == 200)
        {
            health.MaxVal++;
            UpgradeRun = true;
        }
    }

    void GameOver()
    {
        if (curLives == 0)
        {
            curLives = maxLives;
            SceneManager.LoadScene("GameOver");
            Time.timeScale = 0f;
        }
    }

    private void LoadPlayerUpgrades()
    {
        if (health.MaxVal < upgradeProgress.highestHealth)
            health.MaxVal = upgradeProgress.highestHealth;
        if (energy.MaxVal < upgradeProgress.highestEnergy)
            energy.MaxVal = upgradeProgress.highestEnergy;
    }

    private void ForceField()
    {
        if (ForceFieldOn == true && !forceFieldCreated)
        {
            Instantiate(forceFieldEffect, this.gameObject.transform.position, this.gameObject.transform.rotation, this.transform);
            forceFieldCreated = true;
        }
    }
}
