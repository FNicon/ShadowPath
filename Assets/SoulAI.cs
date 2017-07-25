using UnityEngine;
using System.Collections;

public class SoulAI : MonoBehaviour {
	float target;
	bool isGoingDown;
	Rigidbody2D body;
	public float verticalSpeed;

	// Use this for initialization
	void Start () {
		target = transform.position.y-2f;
		isGoingDown = true;
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate() {
		body.velocity = new Vector2(body.velocity.x,verticalSpeed);
	}
}
