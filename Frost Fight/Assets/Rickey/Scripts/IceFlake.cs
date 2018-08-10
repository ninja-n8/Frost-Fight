using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFlake : MonoBehaviour {

	public float downForce = 1f;
	public float sideForce = .1f;

	// Use this for initialization
	void Start () {

		float xForce = Random.Range (-sideForce, sideForce);
		float yForce = Random.Range (downForce / 2f, downForce);

		Vector2 force = new Vector2 (xForce, yForce);

		GetComponent<Rigidbody2D>().velocity = force;

	}
	

}
