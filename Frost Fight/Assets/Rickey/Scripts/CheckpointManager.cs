using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

	public GameObject player;
	public GameObject chkPoint;

    public TimeManager Tm;
    public bool ChkTime;

	public GameObject chkPntDown;
	public GameObject chkPntUp;

	public Transform spawnPoint;
	public Transform checkPoint;

    //reference to PlayerStats (Health, Stamina, etc.)
    public NG_StatManager PlayerStats;

	// Use this for initialization
	void Start () {
        //Find NG_StatManager on Player
        PlayerStats = FindObjectOfType<NG_StatManager>();

		if (chkPntDown.activeInHierarchy == true) {

			chkPntUp.SetActive (false);

		}


        Tm = FindObjectOfType<TimeManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {

			//If the script holder comes into contact with a GameObject labelled as "Player", change the SpawnPoints' transform to match the checkpoints'
			spawnPoint.transform.position = checkPoint.transform.position;

			chkPoint.SetActive (false);
			print ("Checkpoint activated, respawn changed.");
			
			//Change the location of the checkpoint flag to the chkPntYes transform and set checkpoint flag material to green 
			chkPntUp.SetActive (true);
			chkPntDown.SetActive (false);

            if(!ChkTime)
            {
                Tm.tempTime = Tm.startTime;
                ChkTime = true;
            }
		}

	}

	public void PlayerSpawn(){

        Tm.startTime = Tm.tempTime;

        //resetting player health to full
        PlayerStats.Health.CurrentVal = PlayerStats.Health.MaxVal;

		//decemrements player lives by 1
        PlayerStats.CurLives -= 1;

		//Sets players transform.position back to the spawnpoint
        player.transform.position = spawnPoint.transform.position;


    }

}
