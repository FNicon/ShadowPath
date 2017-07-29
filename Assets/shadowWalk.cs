using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowWalk : MonoBehaviour {

	public GameObject shadowRealm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void activate(){
		shadowRealm.SetActive (true);
		GameObject[] allObject = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject anObject in allObject) {
			if (anObject.layer == LayerMask.NameToLayer ("ShadowSurfacesOff")) {
				anObject.layer = LayerMask.NameToLayer ("ShadowSurfacesOn");
			}
		}
	}

	void deactivate(){
		shadowRealm.SetActive (true);
		GameObject[] allObject = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject anObject in allObject) {
			if (anObject.layer == LayerMask.NameToLayer ("ShadowSurfacesOn")) {
				anObject.layer = LayerMask.NameToLayer ("ShadowSurfacesOff");
			}
		}
	}
}
