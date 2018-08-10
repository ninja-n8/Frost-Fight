using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyD : MonoBehaviour {

    public GameObject DamageEnergy;

    public NG_StatManager Stat;

	// Use this for initialization
	private void Start ()
    {
        Stat = FindObjectOfType<NG_StatManager>();
    }
	
	// Update is called once per frame
	public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Stat.Energy.CurrentVal--;

            Destroy(gameObject);
        }
		
	}
}
