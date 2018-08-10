using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;

    public LevelProgress LP;

   

    void Awake()
    {
        mainMenu.SetActive(true);
        LP = FindObjectOfType<LevelProgress>();
        
    }


    void OnStart()
    {
     
    }

   
    //Hub
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    //Only Unlocked Level
    public void PlayScene2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


    //Locked/Unlockable Levels
    public void PlayScene3()

    {
        if (LP.World1Lvl2Unlock == true)
        {
           SceneManager.LoadScene("Tutorial 1-2");
            Debug.Log("Does work?");
        }
    }

    public void PlayScene4()

    {
        if (LP.World1Lvl3Unlock == true)
        {
            SceneManager.LoadScene("Tutorial 1-3");

        }

    }
    public void PlayScene5()

    {
        if (LP.World2Lvl1Unlock == true)
        {
            SceneManager.LoadScene("IceLevelW");
        }

    }
    public void PlayScene6()

    {
        if (LP.World2Lvl2Unlock == true)
        {
            SceneManager.LoadScene("IceLevelT");
        }

    }
    public void PlayScene7()

    {
        if (LP.World2Lvl3Unlock == true)
        {
            SceneManager.LoadScene("IceLevelC");
        }

    }
    public void PlayScene8()

    {
        if (LP.World3Lvl1Unlock == true)
        {
            SceneManager.LoadScene("3-1");
        }

    }
    public void PlayScene9()

    {
        if (LP.World3Lvl2Unlock == true)
        {
            SceneManager.LoadScene("3-2 (Redux)");
        }

    }
    public void PlayScene10()

    {
        if (LP.World3Lvl3Unlock == true)
        {
            SceneManager.LoadScene("3-3");
        }

    }
    public void PlayScene11()

    {
        if (LP.World4Lvl1Unlock == true)
        {
            SceneManager.LoadScene("4-1");
        }

    }
    public void PlayScene12()

    {
        if (LP.World4Lvl2Unlock == true)
        {
            SceneManager.LoadScene("4-2");
        }

    }
    public void PlayScene13()

    {
        if (LP.World4Lvl3Unlock == true)
        {
            SceneManager.LoadScene("4-3");
        }

    }




    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}

