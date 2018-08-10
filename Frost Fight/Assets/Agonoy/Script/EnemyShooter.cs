using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

	[SerializeField]
	private GameObject bullet; 
	// Use this for initialization
	void Start () {
		// calls enemy attack function to start shooting in the beginning of the game. 
		StartCoroutine (Attack());
	}
	IEnumerator Attack(){
		yield return new WaitForSeconds (Random.Range(1,3));
		// attaches to bullet to position of shooter

		Instantiate (bullet, transform.position, Quaternion.identity);
		// Begins coruntine
		StartCoroutine( Attack());
	}
}
