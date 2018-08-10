using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_GunPoint : MonoBehaviour {
	public float speed;

	private Rigidbody2D RB;

	private NG_StatManager SM;

	private SoundManager sound;

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();

		RB = FindObjectOfType<Rigidbody2D>();

		SM = FindObjectOfType<NG_StatManager>();

		sound = FindObjectOfType<SoundManager>();
		if (player.transform.localScale.x < 0)
			speed = -speed;

	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * speed);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			sound.hurt();
			SM.Damage();
			Destroy(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}