using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    public Camera MainCam;
    public Camera TeleCam;
    public NG_Player PC;

    //variable to store original movespeed -NG
    private float originalMoveSpeed;

	// Use this for initialization
	void Start () {
        PC = FindObjectOfType<NG_Player>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (MainCam.gameObject.activeInHierarchy == true)
            {
                TeleCam.gameObject.SetActive(true);
                TeleCam.gameObject.transform.localPosition = new Vector3(0, 0, -1);
                MainCam.gameObject.SetActive(false);
                PC.CanShoot = false;
                originalMoveSpeed = PC.MoveSpeed;
                PC.MoveSpeed = 0;
            }
            /*if (TeleCam.gameObject.activeInHierarchy == true)
            {
                MainCam.gameObject.SetActive(true);
                TeleCam.gameObject.SetActive(false);
            }*/
            
        }
        if (Input.GetKey(KeyCode.C))
        {
            if (TeleCam.gameObject.activeInHierarchy == true)
            {
                
                MainCam.gameObject.SetActive(true);
                TeleCam.gameObject.SetActive(false);
                PC.CanShoot = true;
                PC.MoveSpeed = originalMoveSpeed;
            }
        }
    }
}
