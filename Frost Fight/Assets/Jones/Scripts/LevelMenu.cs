using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Tutorial 1-1");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Tutorial 1-2");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Tutorial 1-3");
    }

    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void PlayLevel5()
    {
        SceneManager.LoadScene("Level 5");
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("LadderGame");
    }
}
