using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brightness : MonoBehaviour {

    public static Brightness lights;

	// Use this for initialization
	void Start ()
    {
		
	}

    //Attempt to change brightness;
    private void Awake()
    {
        if (lights)
        
            DestroyImmediate(gameObject);
            else
             {
                DontDestroyOnLoad(gameObject);
                lights = this;
            }
        }
    }


