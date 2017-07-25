using UnityEngine;
using System.Collections;

public class playerRopeSwing : MonoBehaviour {
	//player
	private Rigidbody2D playerBody;
	private Animator playerAnimation;
	private GroundMovement groundScript;

	private Collider2D onRopeArea;
	private BoxCollider2D ropeBoxChecker;
	public LayerMask ropeLayer;

	public float climbingSpeed;
	public float horizontalSpeed;

	public bool isAbleMoveVertical;
	//enable to be able to move horizontally
	public bool isAbleMoveHorizontal;
	//enable to apply force on the carrier (overrides the above)
	public bool isAbleShakeCarrier;

	private bool isClimbing;
	private bool isSlipping;

	//public int countRope;

	//private GameObject previousParent;
	public Rigidbody2D parentBodyRope;
	private Collider2D parentColliderRope;

	//the horizontal offset for climbing the target (0 is recommended), will be set by object to climb
	//private float xOffset = 0f;

	// Use this for initialization
	void Start () {
		playerBody = GetComponent<Rigidbody2D>();
		playerAnimation = GetComponent<Animator>();
		groundScript = GetComponent<GroundMovement>();
		ropeBoxChecker = GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	void Update () {
		if ((parentBodyRope != null) && (updateRopeArea (0, 0, 0, 0))) {
			Snap ();
			playerBody.gravityScale = 0;
		} else {
			playerBody.gravityScale = 1;
			transform.parent = null;
			parentBodyRope = null;
			parentColliderRope = null;
		}
	}

	public bool isClimbRope() {
		return (parentBodyRope != null);
	}

	public void MovementOnRope(float horizontalInput, float verticalInput) {
		//set the switches
		if (verticalInput > 0) {
			isClimbing = true;
			isSlipping = false;
		}
		else if (verticalInput < 0) {
			isClimbing = false;
			isSlipping = true;
		}
		else {
			isClimbing = false;
			isSlipping = false;
		}

		if (isAbleMoveVertical && (verticalInput < 0 || verticalInput > 0)) {
			playerBody.velocity = new Vector2 (playerBody.velocity.x, climbingSpeed * verticalInput);
		}
		if (isAbleMoveHorizontal && horizontalInput != 0) {
			playerBody.velocity = new Vector2 (horizontalSpeed * horizontalInput, playerBody.velocity.y);
		}
		if (isAbleShakeCarrier) {
			parentBodyRope.AddForce (new Vector2 (horizontalInput, 0));
		}
	}

	void Snap() {
		playerBody.velocity = new Vector2(0,playerBody.velocity.y);
	}

	public void JumpOff() {
		groundScript.Jump();
		parentBodyRope = null;
		parentColliderRope = null;
		transform.parent = null;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Rope")) {
			if (Input.GetAxisRaw ("Vertical") > 0) {
				//countRope = countRope + 1;
				if (transform.parent == null || transform.parent.position.y < other.transform.position.y) {
					isAbleMoveVertical = true;
					isAbleMoveHorizontal = false;
					isAbleShakeCarrier = true;
				}
				if (transform.parent == null) {
					parentColliderRope = other;
				} else {
					parentColliderRope = transform.parent.GetComponent<Collider2D> ();
				}
				transform.parent = other.transform;
				//xOffset = 0;

				parentBodyRope = other.gameObject.GetComponent<Rigidbody2D> ();
				//onEnterRope (other);
			}
		}
	}

	/*void OnTriggerExit2D(Collider2D other) {
		parentBodyRope = null;
		parentColliderRope = null;
		//countRope = countRope - 1;
		if (transform.parent == other.transform) {
			transform.parent = parentColliderRope.transform;
			int tempValue = 0;
			Collider2D nextParent;
			//for (int i = 0; i < countRope) {
			//	if () {

			//	}
			//}
		}
	}*/

	/*void onEnterRope(Collider2D newRope) {
		if ((parentBody == null) || (parentBody.transform.position.y < newRope.transform.position.y)) {
			if (parentBody == null) {
				previousParent = newRope;
			} else {
				previousParent = transform.parent.gameObject;
			}
			transform.parent = newRope.transform;
			xOffset = 0;
		}
	}*/
	//void onExitRope(Rigidbody2D newRope) {
		//if (transform.parent = 
	//}
	/*
	void SetXOffest(float offset)
	{
	xOffset = offset;
	}

	void SetUpController(float newClimbingSpeed, float newSlippingSpeed, float newHorizontalSpeed, bool slipUp, bool slipDown, bool moveHorizontally, bool shake) {
	//if a passed value is 0, then ignore it
	if (newClimbingSpeed != 0.0)
	climbingSpeed = newClimbingSpeed;
	if (newSlippingSpeed != 0.0)
	slippingSpeed = newSlippingSpeed;
	if (newHorizontalSpeed != 0.0)
	horizontalSpeed = newHorizontalSpeed;
	isSlipsDownABit = slipUp;
	isSlipsUpABit = slipDown;
	isAbleMoveHorizontal = moveHorizontally;
	isAbleShakeCarrier = shake;

	//override canMoveHorizontally if needed
	if (isAbleShakeCarrier)
	//isAbleMoveHorizontal = false;
	//}*/
	Collider2D updateRopeArea (float offsetAX, float offsetAY, float offsetBX, float offsetBY) {
		Vector2 pointA;
		Vector2 pointB;
		Vector2 playerPosition;
		playerPosition = transform.position;
		pointA = new Vector2(playerPosition.x + ropeBoxChecker.offset.x - (ropeBoxChecker.size.x / 2), playerPosition.y + ropeBoxChecker.offset.y - (ropeBoxChecker.size.y / 2));
		pointB = new Vector2(playerPosition.x + ropeBoxChecker.offset.x + (ropeBoxChecker.size.x / 2), playerPosition.y + ropeBoxChecker.offset.y + (ropeBoxChecker.size.y / 2));
		return (Physics2D.OverlapArea(new Vector2(pointA.x + offsetAX, pointA.y + offsetAY), new Vector2(pointB.x + offsetBX, pointB.y + offsetBY), ropeLayer));
	}
}