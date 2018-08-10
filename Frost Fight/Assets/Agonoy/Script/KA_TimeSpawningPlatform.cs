using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_TimeSpawningPlatform : MonoBehaviour {

	public Transform FloorSpawn;

	public GameObject floorHazard;

	public bool SpawnTime = false;

	public float SpawnNow;

	public float DelaySpawn;

	// Use this for initialization
	void Start () {

		InvokeRepeating ("SpawnPlatform", SpawnNow, DelaySpawn);

	}
	
	// Update is called once per frame
	public void SpawnPlatform(){

		Instantiate (floorHazard, transform.position, transform.rotation);
		if(SpawnTime){

			CancelInvoke ("SpawnPlatform");

		}
	}
}
