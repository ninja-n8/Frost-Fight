using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrail : MonoBehaviour
{
    public Transform trailPoint;
    public GameObject iceTrail;

    public bool spawnStop = false;

    public float timeSpawn;
    public float spawnDelay;

    void Start()
    {
        //at start, repeatedly invoke instantiate the "SpawnHazard" function at a partial delay
        InvokeRepeating("SpawnHazard", timeSpawn, spawnDelay);
    }

    public void SpawnHazard()
    {
        //Instatiate the iceTrail at the trailPoints' position and rotation
        //if (!spawnStop)
            //InvokeRepeating("SpawnHazard", timeSpawn, spawnDelay);
        Instantiate(iceTrail, transform.position, transform.rotation);
        if (spawnStop)
        {
            CancelInvoke("SpawnHazard");
        }

    }
}
