using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour {

  
   
   

	// Calls GiveMeAMinute.
	void Update () {

       // this.gameObject.transform.position = (Vector3.up);

        StartCoroutine (GiveMeAMinute ());


	}
    //Loads Main Menu script within the set timer.
    IEnumerator GiveMeAMinute()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Main Menu");  
            }
}
