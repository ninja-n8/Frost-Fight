using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndTrigger1 : MonoBehaviour {

    public GameObject levelEndUI;

    //public LevelManager levelComplete1;

    // Use this for initialization
  void Start()
    {
        levelEndUI.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")

        {
            //levelComplete1.CompleteLevel1();

            levelEndUI.SetActive(true);

			StartCoroutine (MenuGo());
            
        }
    }
    //Loads the Credits Scene within the set timer.
	IEnumerator MenuGo(){

		print (Time.time);
		yield return new WaitForSeconds (2);
		print (Time.time); 

		SceneManager.LoadScene ("Credits Scene");
	}

}
