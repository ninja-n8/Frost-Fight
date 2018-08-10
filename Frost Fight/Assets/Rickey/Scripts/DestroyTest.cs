using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour {

	public GameObject testTrigger;

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Tripper") {

			Destroy (testTrigger);

		}

		if (other.tag == "Player") {

			Destroy (testTrigger);

		}

	}

}
