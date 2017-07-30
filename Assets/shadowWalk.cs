using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowWalk : MonoBehaviour {

	public GameObject shadowRealm;
	private GameObject player;
	public bool isActive;

	// Use this for initialization
	void Start () {
		player = GetComponent<GameObject> ();
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void activate(){
		shadowRealm.SetActive (true);
		GameObject[] allObject = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject anObject in allObject) {
			if (anObject.layer == LayerMask.NameToLayer ("ShadowSurfacesOff")) {
				anObject.layer = LayerMask.NameToLayer ("ShadowSurfacesOn");
			}
		}
		//player.layer = LayerMask.NameToLayer ("ShadowPlayer");
		isActive = true;
	}

	public void deactivate(){
		shadowRealm.SetActive (true);
		GameObject[] allObject = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject anObject in allObject) {
			if (anObject.layer == LayerMask.NameToLayer ("ShadowSurfacesOn")) {
				anObject.layer = LayerMask.NameToLayer ("ShadowSurfacesOff");
			}
		}
		//player.layer = LayerMask.NameToLayer ("Default");
		isActive = false;
	}
}
