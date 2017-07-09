using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {
	//Move
	public float maxSpeed;
	//Ground
	public LayerMask groundLayer;
	public Transform groundChecker;
	private bool isTouchGround = false;
	private float groundCheckRadius = 0.2f;
	private bool allowMove;
	public int jumpCount;
	public int maxJumpCount;
	//Jump
	public float jumpForce;
	//Player
	private Rigidbody2D playerBody;
	private Animator playerAnimation;
	private bool facingRight;
	
	// Use this for initialization
	void Start () {
		jumpCount = maxJumpCount;
		allowMove = true;
		playerBody = GetComponent<Rigidbody2D>();
		playerAnimation = GetComponent<Animator>();
		facingRight = true;
	}
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate() {
		isOnGroundUpdate ();
		resetJumpCount ();
		jumpAnimation ();
	}
	public void flipFacing() {
		Vector3 newVector;
		
		facingRight = !facingRight;
		newVector = transform.localScale;
		newVector.x = newVector.x * -1;
		transform.localScale = newVector;
	}
	//MOVE
	public void moveHorizontal(float horizontalSpeed) {
		if (allowMove) {
			playerBody.velocity = new Vector2 (horizontalSpeed * maxSpeed, playerBody.velocity.y);
			playerAnimation.SetFloat ("speed", Mathf.Abs (horizontalSpeed));
		}
		if ((horizontalSpeed > 0) && (!facingRight)) {
			flipFacing ();
		} else if ((horizontalSpeed < 0) && (facingRight)) {
			flipFacing ();
		}
	}
	//JUMP
	public void jumpAnimation() {
		playerAnimation.SetBool ("isTouchGround",isTouchGround);
		playerAnimation.SetFloat ("verticalSpeed", playerBody.velocity.y);
	}
	public void jump() {
		if (jumpCount > 0) {
			isTouchGround = false;
			playerAnimation.SetBool ("isTouchGround", isTouchGround); 
			playerBody.AddForce (new Vector2 (0, jumpForce));
			jumpCount = jumpCount - 1;
		}
	}
	//ON GROUND
	public void isOnGroundUpdate() {
		isTouchGround = Physics2D.OverlapCircle (groundChecker.position, groundCheckRadius,groundLayer);
	}
	public bool getTouchGround() {
		return (isTouchGround);
	}
	public bool isFacingRight() {
		return(facingRight);
	}
	public void resetJumpCount() {
		if (isTouchGround) {
			jumpCount = maxJumpCount;
		}
	}
	public void setAllowMove(bool isAllow) {
		allowMove = isAllow;
	}
}
