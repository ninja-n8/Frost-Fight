using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkP : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<NG_Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.localScale = player.transform.localScale =new Vector3(.5f,.5f);
            player.GetComponent<Rigidbody2D>().mass = .5f;
            Destroy(this.gameObject);
        }
        
    }
}
