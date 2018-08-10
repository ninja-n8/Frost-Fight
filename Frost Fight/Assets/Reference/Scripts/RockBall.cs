using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBall : MonoBehaviour {

	public GameObject snowBallBurst;

	public float throwSpeed;

	private Rigidbody2D RB;

    private SoundManager SM;

    private NG_StatManager stat;

    //Getting a reference to the player -NG
    public NG_Player player;

	// Use this for initialization
	void Start () {

		RB = GetComponent<Rigidbody2D>();
        SM = FindObjectOfType<SoundManager>();
        stat = FindObjectOfType<NG_StatManager>();
        //Assigning the player script -NG
        player = FindObjectOfType<NG_Player>();
        SM.snoSound();
        stat.Energy.CurrentVal--;
        stat.Energy.CurrentVal--;
        stat.Energy.CurrentVal--;
        

        //Check to see if the player is facing left -NG
        if (player.transform.localScale.x < 0)
            throwSpeed = -throwSpeed;

	}

	// Update is called once per frame
	void Update () {

        //RB.velocity = new Vector2 (throwSpeed * transform.localScale.x, 0);
        transform.Translate(Vector3.right * Time.deltaTime * throwSpeed);

    }

	void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "noHit")
        {
            return;
        }

        if (other.gameObject.layer == 13)
            return;

        if (other.gameObject.layer == 15)
            return;

        if (other.tag == "Wall"){

			Instantiate (snowBallBurst, transform.position, transform.rotation);

			Destroy (gameObject);

		}

		if(other.tag =="Floor"){

			Instantiate (snowBallBurst, transform.position, transform.rotation);

			Destroy (gameObject);

		}

        if (other.gameObject.tag == "MainCamera")
            return;

        //if (other.gameObject.GetComponent<Glass>() != null)
            //Destroy(other.gameObject);

        Destroy(gameObject);
    }

}
