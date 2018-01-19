using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarBuildButtons : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject[] shipPartsPrefabs;


	// Use this for initialization
	void Start () {
		MouseManager mouseManager = FindObjectOfType<MouseManager> ();
		foreach (GameObject shipPart in shipPartsPrefabs) {
			GameObject buttonGameObject = Instantiate (buttonPrefab, this.transform);
			Text buttonLabel = buttonGameObject.GetComponentInChildren<Text> ();
			buttonLabel.text = shipPart.name;
			Button button = buttonGameObject.GetComponent<Button> ();

			GameObject shipPartForIstantiation = shipPart; //this is created because at every loop it creates a NEW gameObject reference.
			//whitout this, passing "shipPart" to the listener would make every button use the last shipPart of the array.

			button.onClick.AddListener (() => {mouseManager.prefabToSpawn = shipPartForIstantiation;});


		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
