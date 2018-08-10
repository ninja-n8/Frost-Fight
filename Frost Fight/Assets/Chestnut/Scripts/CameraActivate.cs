using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivate : MonoBehaviour {

    public GameObject Acting;
    public Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = FindObjectOfType<Camera>();
        
        
	}
	
	// Update is called once per frame
	void Update () {
        mainCam = FindObjectOfType<Camera>();

        //Adding a null check to check for when the Acting object has been destroyed, and placing Will's logic inside the else statement. -NG
        if (Acting == null)
            return;
        else
        {
            if (mainCam.transform.position.x + 25 > Acting.gameObject.transform.position.x)
            {
                Acting.SetActive(true);
            }
            if (mainCam.transform.position.x + 25 < Acting.gameObject.transform.position.x)
            {
                Acting.SetActive(false);
            }
        }
    }
}
