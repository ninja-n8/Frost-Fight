using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEnergy : MonoBehaviour {

	public GameObject crystalEnergy;

	public int pointsToGive;
	public int energyToGive;

	private ScoreManager theSM;
	public SoundManager Sound;

	// Use this for initialization
	void Start () {

		theSM = FindObjectOfType<ScoreManager> ();
		Sound = FindObjectOfType<SoundManager>();

	}

	//Gives the player 100 points and adds 1 Energy Crystal to the energyCount
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){

			Sound.iceShard();
			theSM.scoreCount += pointsToGive;

			theSM.energyCount += energyToGive;

			Destroy (this.gameObject);

		}
			
	}

}
