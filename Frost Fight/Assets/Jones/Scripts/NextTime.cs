using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextTime : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update ()
    {

        StartCoroutine(GiveMeAnotherMinute());

    }

    IEnumerator GiveMeAnotherMinute()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Main Menu");
    }

}
