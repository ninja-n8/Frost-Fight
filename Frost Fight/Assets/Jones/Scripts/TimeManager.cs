using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float startTime;
    public float tempTime;
    public float maxTime;

    public NG_StatManager TimeoD;
    [SerializeField] private SoundManager soundManager;

    private Text timeText;

    // Use this for initialization
    void Start()
    {
        timeText = GetComponent<Text>();
        TimeoD = FindObjectOfType<NG_StatManager>();
        soundManager = FindObjectOfType<SoundManager>();
        tempTime = startTime;
        maxTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;

        timeText.text = "" + Mathf.Round(startTime);

        TimedMusic();

        if (startTime < 0)
        {
            soundManager.Sounds[7].Stop();
            soundManager.Sounds[8].Stop();
            soundManager.Sounds[0].Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TimedMusic()
    {
        if(Mathf.Round(startTime) == (maxTime * 0.5f))
        {
            soundManager.Sounds[0].Stop();
            soundManager.halfwayMusic();
        }

        if (Mathf.Round(startTime) == (maxTime * 0.25f))
        {
            soundManager.Sounds[7].Stop();
            soundManager.almostDoneMusic();
        }
    }
}
