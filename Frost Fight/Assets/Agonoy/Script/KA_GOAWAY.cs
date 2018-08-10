using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_GOAWAY : MonoBehaviour {

	public GameObject[] plates;

	[SerializeField]

	int numberofplates;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		if(plates[0].gameObject.GetComponent<KA_PressurePlate>().Plate == true && numberofplates == 1){
			this.gameObject.SetActive (false);
		}
	}
}
