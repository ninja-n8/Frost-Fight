using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

	public GameObject enemy;
	//Rigidbody2D rb;
	//public float x; 
	//public float y; 
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Trail"){

			enemy.transform.GetComponent<SpriteRenderer> ().material.color = Color.blue;

			//x = rb.velocity.x = 0;
			//y = rb.velocity.y = 0;


			StartCoroutine (Frozen());

		}

	}

	IEnumerator Frozen(){

		yield return new WaitForSeconds (3); 

		enemy.transform.GetComponent<SpriteRenderer> ().material.color = Color.white;

	}

}
