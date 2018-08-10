using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_FlyingAI : MonoBehaviour {
	// Use this for initialization
	float DirectionY;

	[SerializeField]

	float movespeed = 3f;

	Rigidbody2D RB;

	Vector3 localScale;

	public float moveup;
	public float movedown;

	void Start () {
		localScale = transform.localScale;
		RB = GetComponent<Rigidbody2D> ();
		DirectionY = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < movedown)
			DirectionY = 1f;
		else if (transform.position.y > moveup)
			DirectionY = -1f;
	}

}
