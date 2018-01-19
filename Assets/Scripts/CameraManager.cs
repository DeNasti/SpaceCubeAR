using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Camera cam;
	public Transform cameraRig;

	public float orbitSensitivity = 5;
	public float zoomMultiplyer = 5;
	public bool invertedZoomDirection = true;
	public bool invertedUpRotation = true;
	public bool HoldToOrbit = false;
	public float panSpeed = 5f;

	float minDistance = 2;
	float maxDistance = 25;

	private Vector3 previousMousePosition;

	void Start () {
		if(cam != null)
			cam.GetComponent<Camera> ();

		cameraRig = cam.transform.parent;
	}
	
	void Update(){
		OrbitCamera ();
		ZoomCamera ();
		PanCamera ();
	}


	void PanCamera (){


		Vector3 input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

		Vector3 changeOfPosition = input*panSpeed*Time.deltaTime;
		changeOfPosition = Quaternion.Euler (0,cam.transform.rotation.eulerAngles.y,0) * changeOfPosition;

		Vector3 newPosition = cameraRig.transform.position + changeOfPosition;

		cameraRig.transform.position = newPosition;
	}

	void OrbitCamera () {

		if (Input.GetButtonDown ("Fire2")) {
			previousMousePosition = Input.mousePosition;
		}
			
		if (Input.GetButton("Fire2")) {

			Vector3 currentMousePosition = Input.mousePosition;

			Vector3 mouseMovement = currentMousePosition - previousMousePosition;

			Vector3 rotationAngle = mouseMovement * orbitSensitivity *Time.deltaTime;

			if (invertedUpRotation)
				rotationAngle.y = -rotationAngle.y;
			
			cam.transform.RotateAround (cameraRig.position, cam.transform.up, rotationAngle.x);
			cam.transform.RotateAround (cameraRig.position, cam.transform.right, rotationAngle.y); //* mouseRotationSense);
			//cam.transform.localPosition = posRelativeToRig;

			cam.transform.rotation = Quaternion.LookRotation (-cam.transform.localPosition);

			if(!HoldToOrbit)
				previousMousePosition = currentMousePosition;

		}
		
	}

	void ZoomCamera (){

		float delta = Input.GetAxis ("Mouse ScrollWheel");

		if (invertedZoomDirection)
			delta = -delta;
		
		Vector3 changeOfPosition = cam.transform.localPosition * zoomMultiplyer * delta;

		Vector3 newPosition = cam.transform.localPosition + changeOfPosition;

		newPosition = newPosition.normalized * Mathf.Clamp (newPosition.magnitude, minDistance, maxDistance);

		cam.transform.localPosition = newPosition;
	}

}
