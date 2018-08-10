using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_Projectile : MonoBehaviour {
	public float speed;

	public KA_AIShoot Shoot;

	private Transform player;

	//public GameObject target;

	private Rigidbody2D rb;
	 
	public NG_StatManager Manager;
	// Use this for initializati
	void Start () {
		Manager = FindObjectOfType<NG_StatManager> ();

		rb = GetComponent<Rigidbody2D> ();

		Shoot = FindObjectOfType<KA_AIShoot> ();

		if (Shoot.transform.localScale.x < 0)
			speed = -speed;
		
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		//target = new Vector2 (player.position.x, player.position.y);
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.x>this.transform.position.x){

		rb.velocity = new Vector2 (speed*transform.localScale.x, 0);
		}
		if (player.transform.position.x<this.transform.position.x){

			rb.velocity = new Vector2 (-speed*transform.localScale.x, 0);
		}
		//transform.position = Vector2.MoveTowards (player.position.x, speed * Time.deltaTime);


		if (transform.position.x == player.transform.position.x && transform.position.y == player.transform.position.y) {
			DestroyProjectile ();
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Manager.Health.CurrentVal--;
			DestroyProjectile ();
		}
	}
	void DestroyProjectile(){
		Destroy (gameObject);
	}
}
