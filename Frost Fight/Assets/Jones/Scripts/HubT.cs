using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubT : MonoBehaviour {

    public string HubExit;

    public LevelProgress MM;
    public int LvlNum;

    public void Start()
    {
        MM = FindObjectOfType<LevelProgress>();
    } 

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //&& Input.GetKey(KeyCode.E))
            {
            if(LvlNum == 2) { MM.World1Lvl2Unlock = true;
            }
            if (LvlNum == 3)
            {
                MM.World1Lvl3Unlock = true;
            }
            if (LvlNum == 4)
            {
                MM.World2Lvl1Unlock = true;
            }
            if (LvlNum == 5)
            {
                MM.World2Lvl2Unlock = true;
            }
            if (LvlNum == 6)
            {
                MM.World2Lvl3Unlock = true;
            }
            if (LvlNum == 7)
            {
                MM.World3Lvl1Unlock = true;
            }
            if (LvlNum == 8)
            {
                MM.World3Lvl2Unlock = true;
            }
            if (LvlNum == 9)
            {
                MM.World3Lvl3Unlock = true;
            }
            if (LvlNum == 10)
            {
                MM.World4Lvl1Unlock = true;
            }
            if (LvlNum == 11)
            {
                MM.World4Lvl2Unlock = true;
            }
            if (LvlNum == 12)
            {
                MM.World4Lvl3Unlock = true;
            }
            if (LvlNum == 13)
            {
                MM.bossRush = true;
            }
            if (LvlNum == 14)
            {
                MM.finalBoss = true;
            }
            LevelUnlock();
             }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LevelUnlock ()
    {
        SceneManager.LoadScene(HubExit);
        }

}
