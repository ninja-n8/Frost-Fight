using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour {

    public TimeManager TT;

   

    void Start()
    {
        TT = FindObjectOfType<TimeManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TT.startTime += 10;

            Destroy(gameObject);
        }
    }
}
