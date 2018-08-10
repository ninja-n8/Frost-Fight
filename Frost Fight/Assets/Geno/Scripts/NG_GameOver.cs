using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NG_GameOver : MonoBehaviour
{
    public LevelProgress MyBitch;

    public string sideHoe;

    
    // Use this for initialization
    void Awake()
    {
        MyBitch = FindObjectOfType<LevelProgress>();
        sideHoe = MyBitch.SideHoe;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RageQuit()
    {
        Application.Quit();
    }

    public void LvlRestart()
    {
        SceneManager.LoadScene(sideHoe);
    }
}

