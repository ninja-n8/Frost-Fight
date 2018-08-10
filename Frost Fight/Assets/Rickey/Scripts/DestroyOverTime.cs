using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public GameObject demoTarg;

	public float demoLag;

	// Use this for initialization
	void Start () {

		StartCoroutine (Demolish ());

	}
	
	IEnumerator Demolish(){

		//Take the script holder and destroy it after demoLag timer
		yield return new WaitForSeconds (demoLag);

		GameObject.Destroy (demoTarg);

	}

}
