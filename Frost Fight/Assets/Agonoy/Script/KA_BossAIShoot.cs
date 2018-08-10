using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_BossAIShoot : MonoBehaviour {

	public GameObject GunPoint;

	public GameObject player;

	public GameObject bullet;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		StartCoroutine (SHOOTHIM ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator SHOOTHIM(){

		yield return new WaitForSeconds (1);

		Shoot ();

		StartCoroutine (SHOOTHIM ());
	}
	public void Shoot(){
		GameObject.Instantiate (bullet, GunPoint.transform.position, GunPoint.transform.rotation);
	}
}
