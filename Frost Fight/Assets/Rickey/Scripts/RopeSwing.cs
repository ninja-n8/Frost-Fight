using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour {

	public bool stay;

	public Transform ropeSeg;

	public GameObject player;

	private float rotSet;
	private float playerGrav;

	public PlayerController PC;

	//at start, find the PlayerController script in the game
	void Start () {

		PC = FindObjectOfType<PlayerController> ();

	}

	void Update () {



	}

	//if the script holder comes into contact with a GameObject tagged as "Player", set that GameObjects' rotation to that of the rope and turn off gravity
	void OnTriggerEnter2D(Collider2D collision){

		if (collision.gameObject.tag == "Player") {

			rotSet = player.transform.rotation.z;

			//player.transform.SetParent (ropeSeg.transform);

			playerGrav = player.GetComponent<Rigidbody2D>().gravityScale;
			player.GetComponent<Rigidbody2D> ().gravityScale = 0;
			player.transform.position = ropeSeg.position;

			stay = true;

		}

	}

	//if the script holder comes into contact with a GameObject tagged as "Player", keep the GameObject at that specific location
	void OnTriggerStay2D(Collider2D collision){

		if (stay == true) {

			player.transform.position = ropeSeg.transform.position;

		}

	}

	//if the script holder comes into contact with a GameObject tagged as "Player", set the GameObjects'
	void OnTriggerExit2D(Collider2D collision){

		if (collision.gameObject.tag == "Player") {

			playerGrav = player.GetComponent<Rigidbody2D> ().gravityScale;
			player.GetComponent<Rigidbody2D> ().gravityScale = 1/10;

			stay = false;

		}

	}

}
