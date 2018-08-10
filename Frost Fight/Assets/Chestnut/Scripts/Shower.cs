using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour {

    public bool canSee;
    public GameObject origin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(origin.transform.position, Input.mousePosition,10);
       
        Debug.DrawLine(origin.transform.position, Input.mousePosition, Color.blue);
        if (hit.collider.gameObject.layer==14)
        {
            canSee = true;
        }
        if (canSee == true)
        {
            hit.collider.gameObject.transform.localScale = Vector3.one;
        }
        if (hit.collider.gameObject.layer != 14)
        {
            canSee = false;
        }
    }
}
