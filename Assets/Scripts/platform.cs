using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {
	public bool movingDown;
	public float downTarget;
	public float upTarget;
	public float currentTarget;
	public float maxSpeed;
	public float currentSpeed;
	private float negator;
	private bool isGoingUp;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		isGoingUp = false;
		movingDown = false;
		body = GetComponent<Rigidbody2D> ();
		negator = -1f;
		currentTarget = upTarget;
		currentSpeed = maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate() {
		if (movingDown && transform.position.y <= currentTarget) {
			if (isGoingUp) {	
				negator = negator * -1;
				currentTarget = upTarget;
				movingDown = false;
				currentSpeed = maxSpeed;
			}
		} else if (!movingDown && transform.position.y >= currentTarget) {
			if (!isGoingUp) {
				negator = negator * -1;
				currentTarget = downTarget;
				movingDown = true;
				currentSpeed = maxSpeed;
			}
		} else {
		}
		body.velocity = new Vector2 (0, negator * currentSpeed);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (!isGoingUp) {
			if (other.gameObject.CompareTag ("Player")) {
				isGoingUp = true;
			}
		}
	}
}
