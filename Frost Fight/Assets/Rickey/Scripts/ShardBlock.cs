using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardBlock : MonoBehaviour {

	public GameObject shard;

	//on start, set shard to inactive
	void Start(){

		shard.SetActive (false);

	}

	void OnTriggerEnter2D(Collider2D other){

		//if the script holder comes into contact with a GameObject tagged as "Player", set shard as active and destroy self
		if (other.tag == "Player") {

			shard.SetActive (true);

			Destroy (this.gameObject);

		} 

	}

}
