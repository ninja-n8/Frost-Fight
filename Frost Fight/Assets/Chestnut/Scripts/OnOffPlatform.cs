using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatform : MonoBehaviour {

    public GameObject thisPlat;
    public GameObject lastPlat;

    public bool loop;

	// Use this for initialization
	void Start () {

        loop = true;
        //StartCoroutine(onOff());
	}
	
	// Update is called once per frame
	void Update () {
		if(loop == true)
        {
            StartCoroutine(onOff());

        }
	}

    IEnumerator onOff()
    {
        if (thisPlat.activeInHierarchy)
        {
            yield return new WaitForSeconds(2);
            lastPlat.SetActive(true);
            
            thisPlat.SetActive(false);
        }

        /*if (lastPlat.activeInHierarchy)
        {
            yield return new WaitForSeconds(2);
            thisPlat.SetActive(true);
            yield return new WaitForSeconds(2);
            thisPlat.SetActive(false);
        }*/

    }
}
