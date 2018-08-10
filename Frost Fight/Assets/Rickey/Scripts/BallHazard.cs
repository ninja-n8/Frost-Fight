using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHazard : MonoBehaviour {

	public Transform ballPoint;
	public GameObject ballHaz;

	public bool spawnStop = false;

	public float timeSpawn;
	public float spawnDelay;

	//at start, repeatedly invoke instantiate the "SpawnHazard" function at a partial delay
	void Start (){

		InvokeRepeating ("SpawnHazard", timeSpawn, spawnDelay);



	}

	//Instatiate the ball hazard at the ball points' position and rotation
	public void SpawnHazard(){

		Instantiate (ballHaz, transform.position, transform.rotation); 
		if(spawnStop){

			CancelInvoke ("SpawnHazard");

		}

	}

}
