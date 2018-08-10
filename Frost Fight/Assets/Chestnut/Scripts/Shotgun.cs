using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {

    public float speed;
    public NG_Player player;
    public Vector3 angle;
    public NG_StatManager stats;
    public float NRG;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<NG_Player>();
        stats = FindObjectOfType<NG_StatManager>();
        if (player.transform.localScale.x < 0)
        {
            angle.x = -angle.x;
        }
        stats.Energy.CurrentVal -= NRG;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(angle);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 15)
            return;

        if (other.gameObject.tag == "noHit")
        {
            return;
        }

        if (other.gameObject.tag == "MainCamera")
            return;

        if (other.gameObject.tag != "SGun")
        {
            Destroy(gameObject);
        }

    }
}
