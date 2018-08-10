using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject P1;
	public GameObject P2;
	public GameObject P1Wins;
	public GameObject P2Wins;

	public int P1Life;
	public int P2Life;

	public GameObject[] P1Health;
	public GameObject[] P2Health;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(P1Life <= 0){

			P1.SetActive (false);

			P2Wins.SetActive (true);
		}

		if(P2Life <= 0){

			P2.SetActive (false);

			P1Wins.SetActive (true);
		}

	}

	public void HurtP1(){

		P1Life -= 1;

		for(int i = 0; i < P1Health.Length; i++){

			if(P1Life > i){

				P1Health [i].SetActive (true);

			}

			else{

				P1Health [i].SetActive (false);

			}

		}


	}

	public void HurtP2(){

		P2Life -= 1;

		for(int i = 0; i < P2Health.Length; i++){

			if(P2Life > i){

				P2Health [i].SetActive (true);

			}

			else{

				P2Health [i].SetActive (false);

			}

		}

	}

}
