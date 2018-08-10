using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrow : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<NG_Player>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.localScale = player.transform.localScale=new Vector3(2,2,0);
            player.GetComponent<Rigidbody2D>().mass = 2f;
            Destroy(this.gameObject);
        }
        
    }
}
