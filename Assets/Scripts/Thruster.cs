using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thruster : MonoBehaviour {

	public Rigidbody rb;
	Button boostButton;
	Vector3 thrusterForce ;
	float thrusterPower = 10f;
	bool areEnginesOn = false;

	void Start(){
		rb = this.transform.root.GetChild (0).GetComponent<Rigidbody> ();
		boostButton = GameObject.FindGameObjectWithTag ("Boost").GetComponent<Button>();
		boostButton.onClick.AddListener (toggleEngines);
	}

	void FixedUpdate(){
		if (areEnginesOn) {
			thrusterForce = this.transform.forward * thrusterPower;
			rb.AddForceAtPosition (-thrusterForce, this.transform.position);
		}
	}

	void toggleEngines(){
		Debug.Log("now fqui");
		if (areEnginesOn) {
			areEnginesOn = false;
			Debug.Log ("now false");
		} else {
			areEnginesOn = true;
			Debug.Log("now true");

		}
	}



}
