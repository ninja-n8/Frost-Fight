using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_EnemyBullet : MonoBehaviour {
	void OnCollisonEnter(Collision2D target){
		if (target.gameObject.tag == "Player") {
		}
		if (target.gameObject.tag == "Ground") {
			Destroy (gameObject);
		}
	}
}
