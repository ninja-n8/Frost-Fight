using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierInactive : MonoBehaviour {

	public GameObject Bar;
	public GameObject Lever;

	//At start, set the barrier to active and barrier material to blue
	void Start(){

		Bar.SetActive (true);
		Lever.transform.GetComponent<SpriteRenderer>().material.color = Color.blue;

	}

	void OnTriggerEnter2D(Collider2D other){

		//If the script holder comes into contact with a GameObject labelled as "Player", set barrier to inactive and barrier material to green
		if (other.tag == "Player"){

			Bar.SetActive (false);
			Lever.transform.GetComponent<SpriteRenderer>().material.color = Color.green;

		}

	}
}
