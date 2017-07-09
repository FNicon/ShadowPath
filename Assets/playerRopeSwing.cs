using UnityEngine;
using System.Collections;

public class playerRopeSwing : MonoBehaviour {
	//player
	private Rigidbody2D playerBody;
	private Animator playerAnimation;

	public bool isOnRope;

	// Use this for initialization
	void Start () {
		playerBody = GetComponent<Rigidbody2D>();
		playerAnimation = GetComponent<Animator>();
		isOnRope = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOnRope) {

		}
	}
	void grabRope() {
		isOnRope = true;
	}
	void offRope() {
		isOnRope = false;
	}
}
