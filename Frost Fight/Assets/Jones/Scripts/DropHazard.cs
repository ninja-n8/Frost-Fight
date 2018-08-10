using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHazard : MonoBehaviour {

    public GameObject DroppingObject;
    public GameObject DropPnt;

	// Use this for initialization
	void Start ()
    {
        Instantiate(
          DroppingObject,
          DropPnt.transform.position,
          Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

      

    }
}
