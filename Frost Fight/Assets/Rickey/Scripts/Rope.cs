using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	internal Rigidbody2D RB;

	internal void Start(){

		//On start, grab script holder and add a Rigidbody2D and set isKinematic to true
		this.gameObject.AddComponent<Rigidbody2D> ();
		this.RB = this.gameObject.GetComponent<Rigidbody2D> ();
		this.RB.isKinematic = true;

		int childCount = this.transform.childCount;

		//Give each child item of this script holder a HingeJoint2D and a BoxCollider2D, as well as a Rigidbody2D and enable collision
		for (int i = 0; i < childCount; i++) {

			Transform anchor = this.transform.GetChild (i);

			anchor.gameObject.AddComponent<HingeJoint2D> ();
			anchor.gameObject.AddComponent<BoxCollider2D> ();

			HingeJoint2D hinge = anchor.gameObject.GetComponent<HingeJoint2D> ();

			hinge.connectedBody = 
				i == 0 ? this.RB :
				this.transform.GetChild (i - 1).GetComponent<Rigidbody2D> ();

			//hinge.useSpring = true;

			hinge.enableCollision = true;

		}
			
	}
}
