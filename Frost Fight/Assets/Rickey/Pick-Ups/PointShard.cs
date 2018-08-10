using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShard : MonoBehaviour {
	 

	public GameObject pointShard;

	public int pointsToGive;
	public int shardsToGive;

    public TimeManager TP;

	private ScoreManager theSM;
    public SoundManager Sound;

    //reference to upgrade progress script -NG
    private NG_UpgradeProgress upgradeProgress;

	void Start(){

		theSM = FindObjectOfType<ScoreManager> ();
        Sound = FindObjectOfType<SoundManager>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        TP = FindObjectOfType<TimeManager>();
	}

	//Gives the player 100 points and adds 1 Point Shard to the shardCount
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){
            Sound.iceShard();
			theSM.scoreCount += pointsToGive;

			theSM.shardCount += shardsToGive;
            upgradeProgress.highestShards += shardsToGive;

			Destroy (this.gameObject);
            TP.startTime += 2;
		}
			

	}
	 
}
