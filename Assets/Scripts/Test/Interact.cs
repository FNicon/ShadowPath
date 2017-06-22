using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interact : MonoBehaviour {
	public Text show;
	// Use this for initialization
	void Start () {
		generateText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void generateText() {

	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {

		}
	}
}
