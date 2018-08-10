﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHurt : MonoBehaviour
{
    private NG_StatManager SM;
    private SoundManager sound;

    // Use this for initialization
    void Start()
    {
        SM = FindObjectOfType<NG_StatManager>();
        sound = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sound.hurt();
            //SM.Health.CurrentVal--;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            SM.Health.CurrentVal--;
    }
}
