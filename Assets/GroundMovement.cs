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
	public int jumpCount;
	public int maxJumpCount;
	//Jump
	public float jumpForce;

	//Player
	private Rigidbody2D playerBody;
	private Animator playerAnimation;
	private bool facingRight;

	//new
	public bool takingOff = false;
	public float jumpVel;
	
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
		takingOffUpdate ();
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
		/*
		if (jumpCount > 0) {
			isTouchGround = false;
			playerAnimation.SetBool ("isTouchGround", isTouchGround); 
			playerBody.AddForce (new Vector2 (0, jumpForce));
			jumpCount = jumpCount - 1;
		}
		*/
		/*
		if (!takingOff) {
			takingOff = true;
			playerAnimation.SetBool ("isTouchGround", isTouchGround);
			if(playerBody.velocity.y <= 1){
				playerBody.velocity = new Vector2(playerBody.velocity.x,jumpVel);
			}
			jumpCount = jumpCount - 1;
		}
		*/
		takingOff = true;
		playerAnimation.SetBool ("isTouchGround", isTouchGround);
		if(playerBody.velocity.y <= 1){
			playerBody.velocity = new Vector2(playerBody.velocity.x,jumpVel);
		}
		jumpCount = jumpCount - 1;

	}
	//ON GROUND
	public void isOnGroundUpdate() {
		isTouchGround = Physics2D.OverlapCircle (groundChecker.position, groundCheckRadius,groundLayer);
		/*
		if((bool)Physics2D.OverlapCircle (groundChecker.position, groundCheckRadius,groundLayer)){
			if((bool)Physics2D.OverlapArea(new Vector2(groundChecker.position.x-groundCheckRadius,groundChecker.position.y+groundCheckRadius),new Vector2(groundChecker.position.x+groundCheckRadius,groundChecker.position.y),groundLayer)){
				isTouchGround = false;
			}
			else{
				isTouchGround = true;
			}
		}
		else{
					isTouchGround = false;
		}
		*/
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

	public void takingOffUpdate(){
		/*
		if (takingOff) {
			if (!isTouchGround) {
				takingOff = false;
			}
		}
		*/

	}
}
