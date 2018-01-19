using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	public GameObject prefabToSpawn;
	public LayerMask snapPointLayerMask;
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
		

			if (Physics.Raycast (ray, out hit)) { //i don't feed the layerMask to the raycast, because i want the ray to HIT all objects, not to pass throught
				int maskForThisHitObject = 1 << hit.collider.gameObject.layer;
					 
				if(( maskForThisHitObject & snapPointLayerMask) != 0) // check if the object i clicked is on the proper layer
				{
					Vector3 spawnPoint = hit.collider.transform.position ;
					Quaternion spawnRotation = hit.collider.gameObject.transform.rotation;

					GameObject g = (GameObject) GameObject.Instantiate(prefabToSpawn, spawnPoint, spawnRotation);
					g.transform.SetParent (hit.collider.gameObject.transform);

					//i have to disable the renderer of the node, because if i apply a block on it, it would be a waste of power to render-it
					if(hit.collider.GetComponent<Renderer>() != null)
						hit.collider.GetComponent<Renderer>().enabled = false;

					if(hit.collider.GetComponent<Collider>() != null)
						hit.collider.GetComponent<Collider>().enabled = false;
				}
	
			}
		}
	}

	private void RemovePart(GameObject g){
		//before destroying i have to re-enable the snap point
		if(g.transform.parent.GetComponent<Renderer>() != null)
			g.transform.parent.GetComponent<Renderer>().enabled = true;
		
		if(g.transform.parent.GetComponent<Collider>() != null)
			g.transform.parent.GetComponent<Collider>().enabled = true;

		Destroy (g);
	}
}
