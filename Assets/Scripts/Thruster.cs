using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {

	public Rigidbody rb;
	Vector3 thrusterForce ;
	float thrusterPower = 10f;

	void Start(){
		rb = this.transform.root.GetChild(0).GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		if (Input.GetButton ("Jump")) {
			thrusterForce = this.transform.forward * thrusterPower;
			rb.AddForceAtPosition(-thrusterForce, this.transform.position);
		}
	}



}
