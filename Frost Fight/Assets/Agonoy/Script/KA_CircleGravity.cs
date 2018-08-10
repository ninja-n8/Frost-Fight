using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_CircleGravity : MonoBehaviour {
	[SerializeField]
	Transform circlePlatform;

	private Rigidbody2D rb;

	[SerializeField]
	float gravitationpull;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		Vector2 difference = circlePlatform.transform.position - this.transform.position;
		rb.AddForce((circlePlatform.transform.position - this.transform.position).normalized*
			gravitationpull);
		float angle = Mathf.Atan2 (difference.x, difference.y);
		this.transform.rotation = Quaternion.AngleAxis (angle, transform.up);
	}
}
