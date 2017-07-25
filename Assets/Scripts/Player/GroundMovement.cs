using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {
	//Move
	public float maxSpeed;
	//Ground
	public LayerMask groundLayer;
	public Transform groundChecker;
	public bool isTouchGround = false;
	private float groundCheckRadius = 0.5f;
	private bool allowMove;
	private bool allowFlip;
	public int jumpCount;
	public int maxJumpCount;
	//Jump
	public float jumpForce;
	//Player
	private Rigidbody2D playerBody;
	private Animator playerAnimation;
	private bool facingRight;
	public float jumpVelocity;

	// Use this for initialization
	void Start () {
		jumpCount = maxJumpCount;
		allowMove = true;
		allowFlip = true;
		playerBody = GetComponent<Rigidbody2D>();
		playerAnimation = GetComponent<Animator>();
		facingRight = true;
	}
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate() {
		IsOnGroundUpdate ();
		ResetJumpCount ();
		JumpAnimation ();
	}
	public void FlipFacing() {
		Vector3 newVector;
		
		facingRight = !facingRight;
		newVector = transform.localScale;
		newVector.x = newVector.x * -1;
		transform.localScale = newVector;
	}
	//MOVE
	public void MoveHorizontal(float horizontalSpeed) {
		if (allowMove) {
			playerBody.velocity = new Vector2 (horizontalSpeed * maxSpeed, playerBody.velocity.y);
			playerAnimation.SetFloat ("speed", Mathf.Abs (horizontalSpeed));
		}
		if (allowFlip) {
			if ((horizontalSpeed > 0) && (!facingRight)) {
				FlipFacing ();
			} else if ((horizontalSpeed < 0) && (facingRight)) {
				FlipFacing ();
			}
		}
	}
	//JUMP
	public void JumpAnimation() {
		playerAnimation.SetBool ("isTouchGround",isTouchGround);
		playerAnimation.SetFloat ("verticalSpeed", playerBody.velocity.y);
	}
	public void Jump() {
		/*if (jumpCount > 0) {
			isTouchGround = false;
			playerAnimation.SetBool ("isTouchGround", isTouchGround); 
			playerBody.AddForce (new Vector2 (0, jumpForce));
			jumpCount = jumpCount - 1;
		}*/
		playerAnimation.SetBool ("isTouchGround", isTouchGround);
		if (playerBody.velocity.y <= 1) {
			playerBody.velocity = new Vector2 (playerBody.velocity.x, jumpVelocity);
		}
		jumpCount = jumpCount - 1;
	}
	//ON GROUND
	public void IsOnGroundUpdate() {
		isTouchGround = Physics2D.OverlapCircle (groundChecker.position, groundCheckRadius,groundLayer);
	}
	public bool GetTouchGround() {
		return (isTouchGround);
	}
	public bool IsFacingRight() {
		return(facingRight);
	}
	public void ResetJumpCount() {
		if (isTouchGround) {
			jumpCount = maxJumpCount;
		}
	}
	public void SetAllowMove(bool isAllow) {
		allowMove = isAllow;
	}
	public void SetAllowFlip(bool isAllow) {
		allowFlip = isAllow;
	}
}
