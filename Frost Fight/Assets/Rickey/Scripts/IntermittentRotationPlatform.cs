using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermittentRotationPlatform : MonoBehaviour {

	//This entire script is bullshit

	public GameObject iRPlat;

	[SerializeField]
	private float speed = 0.1f;

	private float rotation_z;

	public bool rotate;

	// Use this for initialization
	void Start () {

		rotate = true;

	}
	
	// Update is called once per frame
	void Update () {

		//rotate the GameObject along the eulerAngle and add 90 degrees to the rotation
		if (rotate == true) {

			rotation_z = transform.rotation.eulerAngles.z;
			rotation_z += 90;

		} 

		//if rotate is false, set it true
		else if (rotate == false) {

			rotate = true;

		}
			
		StartCoroutine (Rotate ());



	}

	IEnumerator Rotate(){

		//wait 2 seconds and rotate, then set rotate to false
		yield return new WaitForSeconds (2);

		iRPlat.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y, rotation_z), Time.time * speed);

		rotate = false;

	}

}
