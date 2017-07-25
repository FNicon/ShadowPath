using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullMovement : MonoBehaviour {
	public float pushSpeed;
	public float pullForce;
	private Rigidbody2D pullObjectBody;
	private GroundMovement groundScript;
	private Rigidbody2D playerBody;
	private Animator playerAnimation;

	// Use this for initialization
	void Start () {
		playerAnimation = GetComponent<Animator> ();
		playerBody = GetComponent<Rigidbody2D>();
		groundScript = GetComponent<GroundMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerAnimation.SetBool ("isGrabObject",isPullingObject());
		if (isPullingObject()) {
			groundScript.SetAllowFlip (false);
			movingObject ();
		}
	}
	void movingObject() {
		Vector2 transformDistance;
		transformDistance = transform.position - pullObjectBody.transform.position;
		float distance = transformDistance.magnitude;
		Vector2 pullDirection = transformDistance.normalized;
		if (distance > 2) {
			//float pullForce = 1;
			float pullForDistance = (distance - 2) / 2.0f;
			if (pullForDistance > 20) {
				pullForDistance = 20;
			}
			pullForce = pullForce + pullForDistance;
			pullObjectBody.velocity = pullDirection * (pullForce);
			//pullObject.rigidbody2D.velocity = pullDirection * (pullForce * Time.deltaTime); /**/
		}
	}
	public void releaseObject() {
		groundScript.SetAllowFlip (true);
		pullObjectBody = null;
	}
	void pullAnimation(float horizontalSpeed) {
		playerAnimation.SetBool ("isMoving",Mathf.Abs(horizontalSpeed) > 0.1);
	}
	public bool isPullingObject() {
		return (pullObjectBody != null);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("PullableObject")) {
			pullObjectBody = other.gameObject.GetComponent<Rigidbody2D>();
			//pullObjectPosition = hitObject.transform;
		}
	}
	public void MoveHorizontal(float horizontalSpeed) {
		playerBody.velocity = new Vector2 (horizontalSpeed * pushSpeed, playerBody.velocity.y);
		playerAnimation.SetFloat ("pushSpeed", horizontalSpeed);
		pullAnimation (horizontalSpeed);
	}
}
