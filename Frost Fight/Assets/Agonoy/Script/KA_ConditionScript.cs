using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_ConditionScript : MonoBehaviour {
	public GameObject ShowCondition;

	// Use this for initialization
	void Start ()
	{
		ShowCondition.SetActive(false);	
	}

	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D player)

	{ if (player.gameObject.tag == "Player")
		{
			ShowCondition.SetActive(true);
			StartCoroutine("GiveMeASec");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Destroy(ShowCondition);
		}


	}

	IEnumerator GiveMeASec()
	{
		yield return new WaitForSeconds(3);
	}
}
