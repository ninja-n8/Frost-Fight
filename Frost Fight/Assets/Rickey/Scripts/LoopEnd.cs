using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopEnd : MonoBehaviour {

	public GameObject endLoop;

	// Use this for initialization
	void Start () {

		endLoop.SetActive(false);

	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {

		//if the script holder comes into contact with a GameObject tagged as "Player", set EndLoop as active
		if (other.tag == "Player") {

			endLoop.SetActive (true);


		}

	}
		
}
