using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_ForceFieldPickup : MonoBehaviour
{
    private NG_Player player;
    private NG_StatManager playerStats;
    private SoundManager sounds;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<NG_Player>();
        playerStats = FindObjectOfType<NG_StatManager>();
        sounds = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerStats.ForceFieldOn = true;
            sounds.iceShard();
            Destroy(this.gameObject);
        }
    }
}
