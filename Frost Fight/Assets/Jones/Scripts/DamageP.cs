using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageP : MonoBehaviour {

   
    public GameObject DamagePickup;

    public NG_StatManager Stat;

    private void Start()
    {
            Stat = FindObjectOfType<NG_StatManager>();

    }


   public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            Stat.Health.CurrentVal--;

            Destroy(gameObject);
        }
    }
}
