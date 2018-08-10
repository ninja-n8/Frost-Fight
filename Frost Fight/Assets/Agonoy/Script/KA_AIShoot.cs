using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_AIShoot : MonoBehaviour {

	public float speed;

	public float stoppingDistance;

	public float retreatDistance;

	private float timeBTWBullets;

	public float startTimeBTWBullets;

	public GameObject projectle;

	public Transform player;

	public Transform BOOM;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		timeBTWBullets = startTimeBTWBullets;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
			transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
		} 
		else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
			transform.position = this.transform.position;
		} 
		else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
			transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
		}
		if (timeBTWBullets <= 0) {
			Instantiate (projectle, BOOM.position, BOOM.rotation);
			timeBTWBullets = startTimeBTWBullets;
		} 
		else {
			timeBTWBullets -= Time.deltaTime;
		}
	}
}
