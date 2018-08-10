﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_JumpingAI : MonoBehaviour {
	float dirX;
    
	[SerializeField]
	float movespeed = 3f; 

	Rigidbody2D rb;

	bool facingRight = true;

	Vector3 localScale;
	public float jumpforce;
	public float walkleft;
	public float walkRight;

    NG_EnemyStatManager enemyStats;

    // Use this for initialization
    void Start () {
        enemyStats = GetComponent<NG_EnemyStatManager>();
        localScale = transform.localScale;
		rb = GetComponent<Rigidbody2D> ();
		dirX = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < walkleft)
			dirX = 1f;
		else if (transform.position.x > walkRight)
			dirX = -1f;
	}
	void FixedUpdate (){
		rb.velocity = new Vector2 (dirX * enemyStats.moveSpeed, rb.velocity.y);
	}
	void LateUpdate(){
		CheckWhereToFace ();
	}
	void CheckWhereToFace(){
		if (dirX > 0)
			facingRight = true;
		else if (dirX < 0)
			facingRight = false;

		/*if (((facingRight) && (localScale.x < 0)) || ((facingRight) && (localScale.x > 0)))
			localScale.x *= -1;
            */
		transform.localScale = localScale;
	}
	void OnTriggerEnter2D (Collider2D col){
		switch (col.tag) {

		case "BigPlatform":
			rb.velocity = new Vector2(rb.velocity.x, jumpforce);
			break;
		case "SmallPlatform":
			rb.velocity = new Vector2 (rb.velocity.x, jumpforce);
			break;
        case "Enemy":
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                break;
        }
	}
	//void OnTriggerExit2D (Collider col2){}
}