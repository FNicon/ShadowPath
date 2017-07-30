using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRealmInvisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool inShadowRealm = false;
		bool foundPlayer = false;
		int i;
		GameObject[] allObject = FindObjectsOfType<GameObject> ();
		for (i = 0; i < allObject.Length && !foundPlayer;) {
			if (allObject [i].name.Equals ("Player")) {
				foundPlayer = true;
			} else {
				i++;
			}
		}
		if(foundPlayer){
			gameObject.GetComponent<SpriteRenderer>().enabled = !allObject[i].GetComponent<shadowWalk>().isActive;
		}
	}
}
