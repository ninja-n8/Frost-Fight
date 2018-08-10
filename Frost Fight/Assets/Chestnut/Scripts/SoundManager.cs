using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] Sounds;
    public static SoundManager SM;
    public Slider volume;
    
    // Use this for initialization
    void Start()
    {
        
    }

    private void Awake()
    {
        if (SM)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            SM = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        volume = FindObjectOfType<Slider>();
        if (volume == null)
            Debug.Log("We are not in the main menu");
        if(volume != null)
        {
            Sounds[0].volume = volume.value;
            Sounds[7].volume = Sounds[8].volume = Sounds[0].volume;
        }
    }

    public void icicleSound()
    {
        Sounds[1].Play();
    }

    public void snoSound()
    {
        Sounds[2].Play();
    }

    public void jumpSound()
    {
        Sounds[3].Play();
    }

    public void iceShard()
    {
        Sounds[4].Play();
    }

    public void hurt()
    {
        Sounds[5].Play();
    }

    public void drip()
    {
        Sounds[6].Play();
    }

    public void halfwayMusic()
    {
        Sounds[7].Play();
    }

    public void almostDoneMusic()
    {
        Sounds[8].Play();
    }
}
