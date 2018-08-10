using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerBit : MonoBehaviour {

	//Credit To Lyndon Jones For Letting Me Use His Script As A Reference//

	private Vector3 PosA;
	private Vector3 PosB;
	private Vector3 NextPos;

	[SerializeField]
	private float speed;

	[SerializeField]
	private Transform bitTransform;
	[SerializeField]
	private Transform nextTransform;

	// Use this for initialization
	void Start () {

		//set the positions for PosA, PosB, and NextPos
		PosA = bitTransform.localPosition;
		PosB = nextTransform.localPosition;
		NextPos = PosB;

	}

	void Update () {
	
		//run the scan function every frame
		Scan ();

	}

	private void Scan(){

		//move the danger bit towards the next position
		bitTransform.localPosition = Vector3.MoveTowards(bitTransform.transform.localPosition, NextPos, speed * Time.deltaTime);

		if (Vector3.Distance (bitTransform.localPosition, NextPos) <= 0.1) {

			ReturnScan ();

		}

	}

	private void ReturnScan(){

		// return the dangerBit to previous position
		NextPos = NextPos != PosA ? PosA : PosB;

	}

}
