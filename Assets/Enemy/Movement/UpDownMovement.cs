using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour {
	public bool movingDown;
	public float downTarget;
	public float upTarget;
	public float currentTarget;
	public float maxSpeed;
	public float currentSpeed;
	private float negator;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		movingDown = true;
		body = GetComponent<Rigidbody2D> ();
		negator = -1f;
		currentTarget = downTarget;
		currentSpeed = maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate() {
		if (movingDown && transform.position.y <= currentTarget) {
			negator = negator * -1;
			currentTarget = upTarget;
			movingDown = false;
			currentSpeed = maxSpeed;
		} else if (!movingDown && transform.position.y >= currentTarget) {
			negator = negator * -1;
			currentTarget = downTarget;
			movingDown = true;
			currentSpeed = maxSpeed;
		} else {
		}
		body.velocity = new Vector2 (0, negator * currentSpeed);
	}
}
