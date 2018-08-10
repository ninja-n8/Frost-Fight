using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour {

	public GameObject snowBallBurst;

	public float throwSpeed;

	private Rigidbody2D RB;

    private NG_StatManager SM;

    public SoundManager sound;

    //Getting a reference to the player -NG
    public NG_Player player;

    // Use this for initialization
    void Start () {

		RB = GetComponent<Rigidbody2D>();
        SM = FindObjectOfType<NG_StatManager>();
        sound = FindObjectOfType<SoundManager>();
        //Assigning the player script -NG
        player = FindObjectOfType<NG_Player>();
        sound.snoSound();
        SM.Energy.CurrentVal--;
        SM.Energy.CurrentVal--;
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

        if (other.tag == "Wall"){

			Instantiate (snowBallBurst, transform.position, transform.rotation);

			Destroy (gameObject);

		}

		if(other.tag =="Floor"){

			Instantiate (snowBallBurst, transform.position, transform.rotation);

		}

        if (other.gameObject.tag == "MainCamera")
            return;

        //goes through enemies -NG
        if (other.gameObject.layer == 8 || other.gameObject.layer == 16)
            Destroy(gameObject);

	}
}
