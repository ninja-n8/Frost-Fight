using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour {

    public PlayerController PC;
    public GameObject[] thisWeapon = new GameObject[3];

	// Use this for initialization
	void Start () {
        PC.snowBall.SetActive(true);
        PC.iceBall.SetActive(false);
        PC.rockBall.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject==thisWeapon[0])
        {
            PC.snowBall.SetActive(true);
            PC.iceBall.SetActive(false);
            PC.rockBall.SetActive(false);
        }
        if (other.gameObject == thisWeapon[1])
        {
            PC.snowBall.SetActive(false);
            PC.iceBall.SetActive(true);
            PC.rockBall.SetActive(false);
        }
        if (other.gameObject == thisWeapon[2])
        {
            PC.snowBall.SetActive(false);
            PC.iceBall.SetActive(false);
            PC.rockBall.SetActive(true);
        }
    }
}
