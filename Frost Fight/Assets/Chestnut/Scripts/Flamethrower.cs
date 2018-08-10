using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour {
    
    public GameObject newFlame;

	// Use this for initialization
	void Start () {
        
		StartCoroutine(fire());
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator fire()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(newFlame);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        
    }
    
}
