using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBoss : MonoBehaviour {

    public float speed;
    public NG_StatManager stats;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        stats = FindObjectOfType<NG_StatManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, speed);
    }

    
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stats.Health.CurrentVal--;
        }
    }
}
