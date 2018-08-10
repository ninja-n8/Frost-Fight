using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject completeDemoUI;
    public Camera miniCam;
 

	// Use this for initialization
	void Start () {
        //miniCam = GameObject.Find("MiniCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	public void RespawnPlayer(){
        Debug.Log ("Player Respawn");
	}

    public void CompleteDemo() {
        completeDemoUI.SetActive(true);
        //miniCam.enabled = false;
        Time.timeScale = 0f;
    }
}
