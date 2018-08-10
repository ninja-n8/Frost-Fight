using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHeart : MonoBehaviour {

	public GameObject crystalHeart;

	public int pointsToGive;
	public int crystalsToGive;

	private ScoreManager theSM;
	public SoundManager Sound;

    //reference to upgrade progress script -NG
    private NG_UpgradeProgress upgradeProgress;

    // Use this for initialization
    void Start () {

		theSM = FindObjectOfType<ScoreManager> ();
		Sound = FindObjectOfType<SoundManager>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
    }


	//gives the player 100 points and adds 1 Crystal Heart to the crystalCount
    void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){

			Sound.iceShard();
			theSM.scoreCount += pointsToGive;

			theSM.heartCount += crystalsToGive;
            upgradeProgress.highestHeartCrystals += crystalsToGive;

			Destroy (this.gameObject);

		}



	}
}
