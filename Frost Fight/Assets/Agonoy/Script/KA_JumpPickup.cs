using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_JumpPickup : MonoBehaviour {
	
	public GameObject Jump;

	bool JumpTrue = false;

	// Use this for initialization
	void Start () {
		Jump = FindObjectOfType<NG_Player> ().gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (JumpTrue == true) 
		{
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			this.gameObject.transform.transform.parent = Jump.gameObject.transform;
			this.gameObject.transform.localPosition = Vector3.zero;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			JumpTrue = true;
			StartCoroutine (JUMPONIT());
		}
	}
	IEnumerator JUMPONIT()
	{
		yield return new WaitForSeconds (2f);
		Destroy (this.gameObject);
	}
}
