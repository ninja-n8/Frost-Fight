using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reveal : MonoBehaviour {

	public GameObject unknown;

	void Start(){

		//on start, set unknown to inactive
		unknown.SetActive (false);

	}

	void OnTriggerEnter2D(Collider2D other){


		//if the script holder comes into contact with a GameObject tagged as "Player", set unknown to active
		if(other.tag == "Player"){
			
				unknown.SetActive (true);

		}

	}

}
