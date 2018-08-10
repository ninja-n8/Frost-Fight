using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGrow : MonoBehaviour {

    private Transform size;

	// Use this for initialization
	void Start () {
        size = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        size.localScale = new Vector3(size.localScale.x*1.1f, size.localScale.y * 1.1f, size.localScale.z * 1.1f);
	}
}
