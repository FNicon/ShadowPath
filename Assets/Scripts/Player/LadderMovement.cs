using UnityEngine;
using System.Collections;

public class LadderMovement : MonoBehaviour
{
	public GroundMovement groundScript;
	private playerController playerScript;
	//Ladder
	public LayerMask ladderLayer;
	private BoxCollider2D ladderBoxChecker;
	//Ladder Speed
	public float climbSpeed;
	public float snapSpeed;
	private Ladder ladder;
	private Collider2D onLadderArea;

	private bool isSnappedToMiddle = false;
	private bool isClimbingLadder = false;
	private bool isLadderSet = false;
	//Player
	private Rigidbody2D playerBody;
	private Animator playerAnimation;

	// Use this for initialization
	void Start() {
		playerScript = GetComponent<playerController> ();
		playerBody = GetComponent<Rigidbody2D>();
		playerAnimation = GetComponent<Animator>();
		ladderBoxChecker = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update() {
		playerAnimation.SetBool("isClimbLadder", isClimbingLadder);

		onLadderArea = updateLadderArea(0, 0, 0, 0);
		if (!isClimbingLadder && onLadderArea) {
			float verticalInput;
			verticalInput = Input.GetAxis("Vertical");
			if ((!isClimbingLadder && (verticalInput > 0.1 || verticalInput < -0.1)) && !(playerScript.immovable)) {
				stickToMiddle();
			}
		}
		if (!onLadderArea && isClimbingLadder) {
			unstickFromLadder();
		}
		if (isClimbingLadder && isLadderSet && playerScript.isInputJump()) {
			unstickFromLadder();
			groundScript.Jump();
		}
	}
	void FixedUpdate() {
		float verticalSpeed;
		float horizontalSpeed;
		verticalSpeed = Input.GetAxis("Vertical");
		horizontalSpeed = Input.GetAxis("Horizontal");

		if (isClimbingLadder) {
			playerBody.gravityScale = 0;
		} else {
			playerBody.gravityScale = 1;
		}
		if (!playerScript.immovable) {
			if (isClimbingLadder && isLadderSet) {
				bool isStillOnLadder;
				isStillOnLadder = true;
				if (verticalSpeed < 0 && ladder.allowMoveDown) {
					if (!ladder.allowFallBottom) {
						isStillOnLadder = updateLadderArea (0, 0, 0, -0.1f);
					}
					if ((!ladder.allowFallBottom) && (!isStillOnLadder)) {
						playerBody.velocity = new Vector2 (playerBody.velocity.x, 0);
					} else {
						playerBody.velocity = new Vector2 (playerBody.velocity.x, verticalSpeed - (climbSpeed));
						playerAnimation.SetFloat ("climbSpeed", Mathf.Abs (verticalSpeed));
					}
				} else if (verticalSpeed > 0 && ladder.allowMoveUp) {
					if (!ladder.allowFallTop) {
						isStillOnLadder = updateLadderArea (0, 0.1f, 0, 0);
					}
					if ((!ladder.allowFallTop) && (!isStillOnLadder)) {
						playerBody.velocity = new Vector2 (playerBody.velocity.x, 0);
					} else {
						playerBody.velocity = new Vector2 (playerBody.velocity.x, verticalSpeed + (climbSpeed));
						playerAnimation.SetFloat ("climbSpeed", Mathf.Abs (verticalSpeed));
					}
				} else {
					playerBody.velocity = new Vector2 (playerBody.velocity.x, 0);
				}
				if (groundScript.GetTouchGround () && (horizontalSpeed < 0 || horizontalSpeed > 0)) {
					unstickFromLadder ();
				} else if (ladder.allowSnapToMiddle) {
					if (isSnappedToMiddle && (verticalSpeed == 0) && ((ladder.allowFallLeft && horizontalSpeed < 0) || (ladder.allowFallRight && horizontalSpeed > 0))) {
						unstickFromLadder ();
					} else {
						if (transform.position.x > (onLadderArea.transform.position.x - 0.05f) && transform.position.x < (onLadderArea.transform.position.x + 0.05f)) {
							playerBody.velocity = new Vector2 (0, playerBody.velocity.y);
							isSnappedToMiddle = true;
						} else {
							Vector2 gotoCenter = (new Vector3 (onLadderArea.transform.position.x, transform.position.y) - transform.position).normalized * snapSpeed;
							playerBody.velocity = new Vector2 (gotoCenter.x, playerBody.velocity.y);
							isSnappedToMiddle = false;
						}
					}
				} else {
					if (horizontalSpeed < 0 && ladder.allowMoveLeft) {
						if (!ladder.allowFallLeft) {
							isStillOnLadder = updateLadderArea (0, 0, -0.1f, 0);
						}
						if ((!ladder.allowFallLeft) && (!isStillOnLadder)) {
							playerBody.velocity = new Vector2 (0, playerBody.velocity.y);
						} else {
							playerBody.velocity = new Vector2 (horizontalSpeed - climbSpeed, playerBody.velocity.y);
							playerAnimation.SetFloat ("climbSpeed", Mathf.Abs (verticalSpeed));
						}
					} else if (horizontalSpeed > 0 && ladder.allowMoveRight) {
						if (!ladder.allowFallRight) {
							isStillOnLadder = updateLadderArea (0.1f, 0, 0, 0);
						}
						if ((!ladder.allowFallRight) && (!isStillOnLadder)) {
							playerBody.velocity = new Vector2 (0, playerBody.velocity.y);
						} else {
							playerBody.velocity = new Vector2 (horizontalSpeed + climbSpeed, playerBody.velocity.y);
							playerAnimation.SetFloat ("climbSpeed", Mathf.Abs (verticalSpeed));
						}
					}
				}
			}
		}
	}
	Collider2D updateLadderArea (float offsetAX, float offsetAY, float offsetBX, float offsetBY) {
		Vector2 pointA;
		Vector2 pointB;
		Vector2 playerPosition;
		playerPosition = transform.position;
		pointA = new Vector2(playerPosition.x + ladderBoxChecker.offset.x - (ladderBoxChecker.size.x / 2), playerPosition.y + ladderBoxChecker.offset.y - (ladderBoxChecker.size.y / 2));
		pointB = new Vector2(playerPosition.x + ladderBoxChecker.offset.x + (ladderBoxChecker.size.x / 2), playerPosition.y + ladderBoxChecker.offset.y + (ladderBoxChecker.size.y / 2));
		return (Physics2D.OverlapArea(new Vector2(pointA.x + offsetAX, pointA.y + offsetAY), new Vector2(pointB.x + offsetBX, pointB.y + offsetBY), ladderLayer));
	}
	void stickToMiddle() {
		isClimbingLadder = true;
		ladder = onLadderArea.GetComponent<Ladder>();
		isLadderSet = true;
		groundScript.SetAllowMove(false);
	}
	void unstickFromLadder() {
		isClimbingLadder = false;
		ladder = null;
		isLadderSet = false;
		isSnappedToMiddle = false;
		groundScript.SetAllowMove(true);
	}
	//public void isClimbLadderUpdate() {
		//isOnLadder = true;
		//animator.SetTrigger("triggerLadder");
	//}
}