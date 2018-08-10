using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTold : MonoBehaviour {

	public float snow;
    public float rock;
    public float ice;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		snow -= Time.deltaTime;

		if(snow < 0){

			Destroy (gameObject);

		}
        ice -= Time.deltaTime;

        if (snow < 0)
        {

            Destroy(gameObject);

        }
        rock -= Time.deltaTime;

        if (snow < 0)
        {

            Destroy(gameObject);

        }

    }
}
