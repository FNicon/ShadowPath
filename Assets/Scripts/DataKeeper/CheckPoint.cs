using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	Animator saveAnimation;
	public GameObject onTouch;
	public CheckPointManager CheckPointData;

	// Use this for initialization
	void Start () {
		saveAnimation = GetComponent<Animator> ();
		onTouch.SetActive (false);
	}
	
	// Update is called once per frame
	void LateUpdate () {
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			onTouch.SetActive (true);
			saveAnimation.SetBool ("isActive", true);
			CheckPointData.onCheckPoint (transform);
		}
	}
}
