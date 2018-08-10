using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    public SwitchHit[] switches;
    public GameObject bossFF;
    public NG_EnemyStatManager bossHP;

	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
       if (switches[0].Exit == true &&
            switches[1].Exit == true &&
            switches[2].Exit == true &&
            switches[3].Exit == true &&
            switches[4].Exit == true &&
            switches[5].Exit == true)
        {
            bossFF.SetActive(false);
            switches[0].turnBack = true;
            switches[1].turnBack = true;
            switches[2].turnBack = true;
            switches[3].turnBack = true;
            switches[4].turnBack = true;
            switches[5].turnBack = true;
        }

       if (switches[0].Exit == false &&
            switches[1].Exit == false &&
            switches[2].Exit == false &&
            switches[3].Exit == false &&
            switches[4].Exit == false &&
            switches[5].Exit == false)
        {
            bossFF.SetActive(true);
        }
       
        else
        {
            //bossFF.SetActive(true);
        }
    }

    
}
