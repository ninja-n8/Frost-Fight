using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMove : MonoBehaviour {

    public BlobBoss BB;
    public Vector3 startPoint;
    [SerializeField] float blobSpeed = 3.5f;

	// Use this for initialization
	void Start () {
        BB = FindObjectOfType<BlobBoss>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BB.speed = blobSpeed;
        }
        /*else
        {
            BB.gameObject.transform.position = startPoint;
            BB.speed = 0;
        }*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            BB.gameObject.transform.position = startPoint;
            BB.speed = 0;
        }
        
    }
}
