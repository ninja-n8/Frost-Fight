using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {

    public NG_EnemyStatManager Boss;
    public bool isDead;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Boss.EnemyHealth.CurrentVal==0)
        {
            isDead = true;
        }
        if (isDead)
        {
            Destroy(this.gameObject);
        }
	}
}
