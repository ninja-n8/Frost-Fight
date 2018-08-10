using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour {

	public GameObject Burst;
	public GameObject Mine;

	public NG_StatManager Stat;

	void Start(){

		Stat = FindObjectOfType<NG_StatManager> ();

	}

	void OnTriggerEnter2D(Collider2D other){

		//if the script holder comes into contact with a GameObject tagged as "Player", destroy the script holder and instatiate snowBurst effect, and decrement player health twice
		if(other.tag == "Player"){

			Stat.Health.CurrentVal--;
			Stat.Health.CurrentVal--;

			Destroy (Mine);

			Instantiate (Burst, transform.position, transform.rotation);

		}

		//if the script holder comes into contact with a GameObject tagged as "RockP", destroy the script holder and instatiate snowBurst effect
		if (other.tag == "RockP") {

			Destroy (Mine);

			Instantiate (Burst, transform.position, transform.rotation);

		}

	}

}
