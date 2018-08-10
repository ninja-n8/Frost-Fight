using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : MonoBehaviour {

	public GameObject crystalBall;

	public int pointsToGive;
	public int spheresToGive;

	private ScoreManager theSM;
	public SoundManager Sound;

	// Use this for initialization
	void Start () {

		theSM = FindObjectOfType<ScoreManager> ();
		Sound = FindObjectOfType<SoundManager>();

	}
		
	//Gives the player 100 points and adds 1 Crystal Ball to the sphereCount
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){

			Sound.iceShard();
			theSM.scoreCount += pointsToGive;

			theSM.ballCount += spheresToGive;

			Destroy (this.gameObject);

		}

	}

}
