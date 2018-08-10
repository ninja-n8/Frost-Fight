using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour {

	public Transform telePoint;

	public GameObject player;

	//if the script holder comes into contact with a GameObject tagged as "Player", set that GameObjects' transform.position to the teleport point
	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {

			player.transform.position = telePoint.transform.position;

		}

	}

}
