using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour {

	public GameObject Burst;
	public GameObject Ball;

	public SoundManager Sound;

	public NG_StatManager Stat;

	void Start(){

		Sound = FindObjectOfType<SoundManager> ();
        Stat = FindObjectOfType<NG_StatManager>();

	}

	//If the script holder hits another GameObject, check these IF statements
	void OnTriggerEnter2D(Collider2D other){

		//if the script holder hits a GameObject tagged as "Destroy", then the script holder gets destroyed and instatiates the snowBurst particle effect
		if(other.tag == "Destroy"){

			Destroy (Ball);

			Instantiate (Burst, transform.position, transform.rotation);

		}

		//if the script holder hits a GameObject tagged as "Player", then the script holder gets destroyed and instatiates the snowBurst particle effect
        if (other.tag == "Player")
        {

            Stat.Health.CurrentVal--;
            Sound.hurt();

            Destroy(Ball);

            Instantiate(Burst, transform.position, transform.rotation);

        }

    }
}
