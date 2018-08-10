using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStill : MonoBehaviour {

	// Use this for initialization
	void OnCollisionStay2D (Collision2D other) {

		//if the script holder comes into contact with a GameObject tagged as "Player", parent that GameObject
		if (other.gameObject.tag == "Player") {

			other.transform.SetParent (this.gameObject.transform, true);

		}

	}

	void OnCollisionExit2D (Collision2D other){

		//if the script holder comes into contact with a GameObject tagged as "Player", set parent to null
		if (other.gameObject.tag == "Player") {

			other.transform.parent = null;

		}

	}

}
