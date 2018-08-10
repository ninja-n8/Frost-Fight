using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour {

	public GameObject doorWay;
	public GameObject doorWayTrigger;

	//At start, set the doorway trigger to active
	void Start(){

		doorWayTrigger.SetActive (true);

	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == "Player"){

			//If the script holder comes into contact with a GameObject labelled as "Player", if the doorway is inactive, set it active and set doorway trigger inactive
			if (doorWay.activeInHierarchy == false) {

				doorWay.SetActive (true);
				doorWayTrigger.SetActive (false);


			}

			//If the script holder comes into contact with a GameObject labelled as "Player", if the doorway is active, set it active
			else if (doorWay.activeInHierarchy == true) {

				doorWay.SetActive (false);

			}

		}
			
	}
		
}
