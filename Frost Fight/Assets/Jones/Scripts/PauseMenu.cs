using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject UpgradeMenuUI;
    public CheckpointManager ChkRef;
    public GameObject player;
    public Transform spawnPoint;



    void Awake()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void CheckRef()
    {
        ChkRef = FindObjectOfType<CheckpointManager>();
    }

    // Resume Button activates to continue game when paused
   public void Resume ()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    // Pause Button activates suspending game
    void Pause ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    // Loads Main Menu from the Menu Button
    public void LoadMenuP()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    // Quits the application when pressed
    public void QuitGameP()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void UpgradeMenu()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void NextLevelLoad ()
    {
        Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    //Allowing the player to buy Upgrades in between levels -NG
    public void UpgradeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UpgradeScene");
    }

    public void LoadCredits ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits Scene");
    }

    public void GetLastCheckpoint()
    {
        if (player.transform.position != spawnPoint.transform.position)
        {
            ChkRef.PlayerSpawn();
        }
    }

      public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }
    
}

