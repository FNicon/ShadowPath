using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour {
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (body.bodyType == RigidbodyType2D.Dynamic) {
			body.bodyType = RigidbodyType2D.Kinematic;
		}
	}
}
