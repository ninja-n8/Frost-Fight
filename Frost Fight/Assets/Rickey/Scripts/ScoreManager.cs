using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text shardText;
	public Text heartText;
	public Text energyText;
	public Text ballText;
	public Text gemText;

    public float scoreCount;
	public float shardCount;
	public float heartCount;
	public float energyCount;
	public float ballCount;
	public float gemCount;

    //Getting a static reference to this object so we can add DontDestroyOnLoad -NG
    public static ScoreManager scoreManager;

    private void Awake()
    {
        if (scoreManager)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            scoreManager = this;
        }
    }

    // Use this for initialization
    void Start () {

		//at start, set heartText, energyText, ballText, and gemText to inactive
		heartText.enabled = false;
		energyText.enabled = false;
		ballText.enabled = false;
		gemText.enabled = false;

	}

	// Update is called once per frame
	void Update () {

		//set the point display to what the score, shard, heart, energy, ball, or gemCount is
		scoreText.text = "Score: " + scoreCount;
		shardText.text = "Shards: " + shardCount;
		heartText.text = "Crystals: " + heartCount;
		energyText.text = "Energy: " + energyCount;
		ballText.text = "Spheres: " + ballCount;
		gemText.text = "Gems: " + gemCount;

		//if heartCount is greater than or equal to 1 set heartText to active
		if (heartCount >= 1) {

			heartText.enabled = true;

		}

		//if energyCount is greater than or equal to 1 set energyText to active
		if (energyCount >= 1) {

			energyText.enabled = true;

		}

		//if ballCount is greater than or equal to 1 set ballCount to active
		if (ballCount >= 1) {

			ballText.enabled = true;

		}

		//if gemCount is greater than or equal to 1 set gemCount to active
		if (gemCount >= 1) {

			gemText.enabled = true;

		}

    }
}
