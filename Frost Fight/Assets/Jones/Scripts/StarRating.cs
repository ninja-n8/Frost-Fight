using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRating : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public float starRate1;
    public float starRate2;
    public float starRate3;

    public TimeManager time;
    private SoundManager soundManager;

    // Use this for initialization
    void Start()
    {
        time = FindObjectOfType<TimeManager>();
        soundManager = FindObjectOfType<SoundManager>();
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            soundManager.Sounds[7].Stop();
            soundManager.Sounds[8].Stop();
            soundManager.Sounds[0].Play();

            if (time.startTime > starRate1)
            {
                Star1.SetActive(true);
            }

            if (time.startTime > starRate2)
            {
                Star2.SetActive(true);
            }

            if (time.startTime > starRate3)
            {
                Star3.SetActive(true);
            }
            time.gameObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
