using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    public float speed;
    public NG_StatManager stat;
    public GameObject player;
    public SoundManager SM;
    public float myScale;
    public float pScale;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        stat = FindObjectOfType<NG_StatManager>();
        SM = FindObjectOfType<SoundManager>();
       
	}
	
	// Update is called once per frame
	void Update () {
        
        myScale = this.gameObject.transform.localScale.x;
        pScale = player.transform.localScale.x;
        if (this.gameObject.transform.position.x < player.transform.position.x)
        {
            myScale = 1;
        }
        if (this.gameObject.transform.position.x > player.transform.position.x)
        {
            myScale = -1;
        }
        if (myScale!=pScale)
        {
            speed = 0;
            
        }
        if (myScale == pScale)
        {
            speed = .1f;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
        }
		
	}
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stat.Damage();
            SM.hurt();

            GameObject.Destroy(this.gameObject);
        }
    }
}
