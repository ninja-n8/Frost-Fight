using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakdown : MonoBehaviour{

    public GameObject breaks;

    //Deletes game object from scene/breaks
     public void Update()
    {
        if (Input.GetKeyDown (KeyCode.E))
        {
            Destroy(breaks);
        }
    }

}