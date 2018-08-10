using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_Rotation : MonoBehaviour {
	// Use this for initialization
	public GameObject rotateplatform;

	public float speed = 10f;

	public bool isEnabled = true;

	void Start () {
		rotateplatform = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate( new Vector3(0, 0, speed) * Time.deltaTime); 
		//StartCoroutine (Rotation());

		//Quaternion newRotation = Quaternion.Euler (0, 0, Random.Range(0, 0));

		//transform.rotation = Quaternion.Lerp (transform.rotation, newRotation, speed * Time.deltaTime);
	}

	//IEnumerator Rotation(){
		//yield return new WaitForSeconds (UnityEngine.Random.Range (5f, 10f));

		//speed = Random.Range (5, 10);
		//if (isEnabled) 
		//{
		//	StartCoroutine (Rotation ());
		//}
	//}
}
