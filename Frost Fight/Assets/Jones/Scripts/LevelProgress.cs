using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour {

    public static LevelProgress LP;

    public string curLevel;

    public string SideHoe;



    [SerializeField]
    public bool World1Lvl2Unlock;
    [SerializeField]
    public bool World1Lvl3Unlock;

    [SerializeField]
    public bool World2Lvl1Unlock;
    [SerializeField]
    public bool World2Lvl2Unlock;
    [SerializeField]
    public bool World2Lvl3Unlock;

    [SerializeField]
    public bool World3Lvl1Unlock;
    [SerializeField]
    public bool World3Lvl2Unlock;
    [SerializeField]
    public bool World3Lvl3Unlock;

    [SerializeField]
    public bool World4Lvl1Unlock;
    [SerializeField]
    public bool World4Lvl2Unlock;
    [SerializeField]
    public bool World4Lvl3Unlock;

    public bool bossRush;

    public bool finalBoss;

    public bool timer;

    public TimeManager TM;

    // Use this for initialization
    void Awake() {

        
        

        if (LP) DestroyImmediate(gameObject);

        else
        {
           DontDestroyOnLoad(gameObject);
            LP = this;
        }



    }
	
	// Update is called once per frame
	void Update () {

        curLevel = SceneManager.GetActiveScene().name;
        if (curLevel == "Tutorial 1-1")
        {
            SideHoe = curLevel;
        }
        if (curLevel == "Tutorial 1-2")
        {
            SideHoe = curLevel;
        }
        if (curLevel == "Tutorial 1-3")
        {
            SideHoe = curLevel;
        }
        if (curLevel == "IceLevelW")
        {
            SideHoe = curLevel;
        }
        if (curLevel == "IceLevelT")
        {
            SideHoe = curLevel;
        }
        if (curLevel == "IceLevelC")
        {
            SideHoe = curLevel;
        }


        if (Input.GetKeyDown(KeyCode.U))
        {

            World1Lvl2Unlock = true;
            World1Lvl3Unlock = true;
            World2Lvl1Unlock = true;
            World2Lvl2Unlock = true;
            World2Lvl3Unlock = true;
            World3Lvl1Unlock = true;
            World3Lvl2Unlock = true;
            World3Lvl3Unlock = true;
            World4Lvl1Unlock = true;
            World4Lvl2Unlock = true;
            World4Lvl3Unlock = true;
            bossRush = true;
            finalBoss = true;
        }

        TM = FindObjectOfType<TimeManager>();

        if (timer==false)
        {
            TM.gameObject.SetActive(false);
        }
	}
    public void TimeToggle()
    {
        if (timer==true)
        {
            timer=false;
        }
        else if (timer == false)
        {
            timer = true;
        }
    }
}
