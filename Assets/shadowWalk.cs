using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowWalk : MonoBehaviour {

	public GameObject shadowRealm;
	private GameObject player;
	public bool isActive = false;
	public string ping = "wow";

	// Use this for initialization
	void Start () {
		player = GetComponent<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void activate(){
		shadowRealm.SetActive (true);
		GameObject[] allObject = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject anObject in allObject) {
			if (anObject.layer == 14) {
				ping = anObject.name;
				anObject.layer = 15;
			}
		}
		//player.layer = LayerMask.NameToLayer ("ShadowPlayer");
		isActive = true;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = 2;
	}

	public void deactivate(){
		shadowRealm.SetActive (false);
		GameObject[] allObject = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject anObject in allObject) {
			if (anObject.layer == 15) {
				ping = anObject.name;
				anObject.layer = 14;
			}
		}
		//player.layer = LayerMask.NameToLayer ("Default");
		isActive = false;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = 0;
	}
}
