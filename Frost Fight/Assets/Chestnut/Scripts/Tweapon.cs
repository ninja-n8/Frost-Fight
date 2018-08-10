using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweapon : MonoBehaviour {

    public float speed;
    private Rigidbody2D RB;
    private NG_StatManager SM;
    private SoundManager sound;
    public bool boss;

	// Use this for initialization
	void Start () {
        RB = FindObjectOfType<Rigidbody2D>();
        SM = FindObjectOfType<NG_StatManager>();
        sound = FindObjectOfType<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player"&&!boss)
        {
            sound.hurt();
            SM.Health.CurrentVal--;
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
