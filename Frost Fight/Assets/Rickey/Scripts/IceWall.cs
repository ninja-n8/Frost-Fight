using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.tag == "RockP") {

			Destroy(this.gameObject);

		}

	}

}
