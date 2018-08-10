using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

	public LevelManager lvlMngr;
	public CheckpointManager chkPntMngr;
    

	// Use this for initialization
	void Start () {

		lvlMngr = FindObjectOfType<LevelManager> ();
		chkPntMngr = FindObjectOfType<CheckpointManager> ();

	}
	
	// Update is called once per frame
	void Update () {



	}

	public void OnTriggerEnter2D(Collider2D other){

		//If the script holder comes into contact with a GameObject labelled as "Player", call the listed functions from the listed public scripts
		if (other.tag == "Player") {

			lvlMngr.RespawnPlayer ();

			chkPntMngr.PlayerSpawn ();

		}

	}

}
