using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGem : MonoBehaviour {

	public GameObject frostGem;

	public int pointsToGive;
	public int gemsToGive;

	private ScoreManager theSM;
	public SoundManager Sound;

	// Use this for initialization
	void Start () {

		theSM = FindObjectOfType<ScoreManager> ();
		Sound = FindObjectOfType<SoundManager>();

	}

	//Gives the player 100 points and adds 1 Frost Gem to the gemCount
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){

			Sound.iceShard();
			theSM.scoreCount += pointsToGive;

			theSM.gemCount += gemsToGive;

			Destroy (this.gameObject);

		}

	}

}
