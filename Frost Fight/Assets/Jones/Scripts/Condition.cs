using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour {

    public GameObject ShowCondition;

	// Use this for initialization
	void Start ()
    {
        ShowCondition.SetActive(false);	
	}
	
	//Sets condition text active while  player is in Collider. 
	void OnTriggerEnter2D (Collider2D player)

    { if (player.gameObject.tag == "Player")
        {
            ShowCondition.SetActive(true);
            //StartCoroutine("GiveMeASec");
        }
	}

    //Destroys the condition on player exit.
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
          ShowCondition.SetActive(false);
        }


    }
    //Destroys condition within the set timer.
  /* IEnumerator GiveMeASec()
    {
        yield return new WaitForSeconds(6);
     
    }*/

}
