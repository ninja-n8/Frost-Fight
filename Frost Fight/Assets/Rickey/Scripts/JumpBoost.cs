using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour {

	public PlayerController control;

	//find the playercontroller script in the game
	void Start(){

		control = FindObjectOfType<PlayerController> ();

	}

	void OnTriggerEnter2D(Collider2D other){

		//if the script holder comes into contact with a GameObject tagged as "Player", set PlayerController.JumpMultiplier to 1
		if (other.tag == "Player") {

			control.JumpMultiplier = 1f;

		}

	}

	void OnTriggerExit2D(Collider2D other){

		//if the script holder leaves contact with a GameObject tagged as "Player", set PlayerController.JumpMultiplier to 2
		if (other.tag == "Player") {

			control.JumpMultiplier = 2f;

		}

	}

		
}
