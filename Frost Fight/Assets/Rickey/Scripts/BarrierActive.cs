using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierActive : MonoBehaviour {

	public GameObject bossBar;

	//at start, set the barrier to inactive
	void Start(){

		bossBar.SetActive (false);

	}

	void OnTriggerEnter2D(Collider2D other){

		//If the script holder comes into contact with a GameObject labelled as "Player", set barrier to active
		if (other.tag == "Player"){

			bossBar.SetActive (true);

		}

	}
}
