using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDoor : MonoBehaviour {

    public bool w2door;
    public bool w3door;
    public bool w4door;
    public bool w5door;
    public bool w6door;
    public LevelProgress LP;


    // Use this for initialization
    void Start () {
        LP = FindObjectOfType<LevelProgress>();
	}
	
	// Update is called once per frame
	void Update () {
        if (LP.World2Lvl1Unlock == true && w2door == true)
        {
            Destroy(this.gameObject);
        }
        if (LP.World3Lvl1Unlock == true && w3door == true)
        {
            Destroy(this.gameObject);
        }
        if (LP.World4Lvl1Unlock == true && w4door == true)
        {
            Destroy(this.gameObject);
        }
        if (LP.bossRush == true && w5door == true)
        {
            Destroy(this.gameObject);
        }
        if (LP.finalBoss == true && w6door == true)
        {
            Destroy(this.gameObject);
        }
    }
}
