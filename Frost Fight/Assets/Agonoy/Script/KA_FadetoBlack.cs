using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KA_FadetoBlack : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadespeed = 0.8f;

	private int drawdepth = -1000;
	private float alpha = 1.0f;
	private int fadedir = -1;

	void OnGUI()
	{
		
		alpha += fadedir * fadespeed * Time.deltaTime;

//		alpha = Mathf.Clamp (alpha);

		// Set color of GUI 
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha); // sets alpha 
		GUI.depth = drawdepth;												  // make black texture render
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height),fadeOutTexture); // draw texture to fit entire screen

	}
	public float BeginFade(int direction)
	{
		// sest faddir to direction parameter making scene fade in if -1 or out if 1;
		fadedir = direction;
		return (fadespeed); // return fadespeed var so its easy to time Application.LoadLevel();
	}
	// Called when a level is loaded. Takes l	oaded level index (int) as parameter so u can limit fade in scenes
	void OnLevelWasLoaded ()
	{					// Use to if alpha is not set to 1 by default
		BeginFade (-1); // call function
	}
}
