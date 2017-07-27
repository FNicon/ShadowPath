using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeMovement : MonoBehaviour {
	private bool isOnLedge;
	public float radiusCheck;
	public CircleCollider2D circleCollisionChecker;
	public LayerMask collisionLayer;
	private Rigidbody2D playerBody;
	private Animator playerAnimation;

	// Use this for initialization
	void Start () {
		playerBody = GetComponent<Rigidbody2D> ();
		playerAnimation = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*Collider2D checkArea (float offsetAX, float offsetAY, float offsetBX, float offsetBY) {
		Vector2 pointA;
		Vector2 pointB;
		Vector2 playerPosition;
		playerPosition = transform.position;
		pointA = new Vector2(playerPosition.x + boxCollisionChecker.offset.x - (boxCollisionChecker.size.x / 2), playerPosition.y + boxCollisionChecker.offset.y - (boxCollisionChecker.size.y / 2));
		pointB = new Vector2(playerPosition.x + boxCollisionChecker.offset.x + (boxCollisionChecker.size.x / 2), playerPosition.y + boxCollisionChecker.offset.y + (boxCollisionChecker.size.y / 2));
		return (Physics2D.OverlapArea(new Vector2(pointA.x + offsetAX, pointA.y + offsetAY), new Vector2(pointB.x + offsetBX, pointB.y + offsetBY), collisionLayer));
	}*/
	Collider2D checkWall () {
		return(Physics2D.OverlapCircle (new Vector2 (transform.position.x,transform.position.y), radiusCheck, collisionLayer));
	}
}
