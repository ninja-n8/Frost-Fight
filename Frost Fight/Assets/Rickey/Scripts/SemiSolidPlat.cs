using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiSolidPlat : MonoBehaviour {

	public GameObject semiSolid;

	void OnTriggerEnter2D (Collider2D other) {

		//if the script holder comes into contact with a GameObject tagged as "Player" and semiSolid is inactive, set semiSolid to active
		if (other.tag == "Player" && semiSolid.activeInHierarchy == false) {

			semiSolid.SetActive (true);

		}

		//if the script holder comes into contact with a GameObject tagged as "Player", give semiSolid a BoxCollider2D
		if (other.tag == "Player") {

            //semiSolid.AddComponent<BoxCollider2D> ();
            semiSolid.tag = "Ground";
		}

	}

	void OnTriggerExit2D (Collider2D other){

		//if the script holder leaves contact with a GameObject tagged as "Player", destroy the BoxCollider2D
		if (other.tag == "Player") {

			//UnityEngine.Object.Destroy (semiSolid.GetComponent<BoxCollider2D>());
            semiSolid.tag = "Through";
		}
		
	}

}
