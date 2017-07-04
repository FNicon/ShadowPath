using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerController : MonoBehaviour {
	public float maxSpeed;
	public LayerMask groundLayer;
	public LayerMask ladderLayer;
	public Transform groundChecker;
	public BoxCollider2D ladderBoxChecker;
	public float jumpForce;
	public Text textcounter;
	public Text interact;
	public float stairSpeed;
	private int soulCounter;
	private Rigidbody2D playerBody;
	private Animator playerAnimation;
	private bool facingRight;
	private bool touchGround = false;
	private float groundCheckRadius = 0.2f;
	private bool isOnLadder = false;
	private GameObject otherObject;
	//public Transform launchPod;
	//public GameObject missile;
	//float fireRate = 0.5f;
	//float reloadTime = 0f;

	// Use this for initialization
	void Start () {
		playerBody = GetComponent<Rigidbody2D>();
		playerAnimation = GetComponent<Animator>();
		facingRight = true;
		soulCounter = 0;
		setText ();
		ladderBoxChecker = GetComponent<BoxCollider2D>();
		otherObject = null;
	}

	void Update(){

		if ((touchGround) && isInputJump ()) {
			touchGround = false;
			jump ();
		}
		if  (isInputFire1()){
			//firing();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float horizontalSpeed;
		float verticalSpeed;
		horizontalSpeed = inputHorizontal ();
		verticalSpeed = inputVertical ();

		isOnGroundUpdate ();
		jumpAnimation ();

		moveHorizontal (horizontalSpeed);

		if (isOnLadder == true) {
			playerBody.gravityScale = 0;
			playerBody.velocity = new Vector2 (playerBody.velocity.x,verticalSpeed*stairSpeed);
			isOnLadderUpdate ();
		}
	}
	void flipFacing() {
		Vector3 newVector;

		facingRight = !facingRight;
		newVector = transform.localScale;
		newVector.x = newVector.x * -1;
		transform.localScale = newVector;
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			soulCounter = soulCounter + 1;
			setText ();
		} else if (other.gameObject.CompareTag ("Interact")) {
			if (isInputFire1 ()) {
				otherObject = other.gameObject;
			}
		} else if (other.gameObject.CompareTag ("Ladder")) {
			isOnLadder = true;
		} else if (other.gameObject.CompareTag ("Rope")) {

		} else if (other.gameObject.CompareTag ("Enemy")) {
			Destroy(gameObject);
		}
	}
	void setText() {
		textcounter.text = soulCounter.ToString();
	}
	void generateText() {
		interact.text = soulCounter.ToString();
	}
	void isOnLadderUpdate() {
		Vector2 pointA;
		Vector2 pointB;
		float playerX;
		float playerY;
		playerX = transform.position.x;
		playerY = transform.position.y;
		pointA = new Vector2 (playerX + ladderBoxChecker.offset.x - (ladderBoxChecker.transform.position.x/2), playerY + ladderBoxChecker.offset.y - (ladderBoxChecker.transform.position.y/2));
		pointB = new Vector2 (playerX + ladderBoxChecker.offset.x + (ladderBoxChecker.transform.position.x/2), playerY + ladderBoxChecker.offset.y + (ladderBoxChecker.transform.position.y/2));
		isOnLadder = Physics2D.OverlapArea (pointA,pointB,ladderLayer);
		if (!isOnLadder) {
			playerBody.gravityScale = 1;
		}
	}
	void isOnGroundUpdate() {
		touchGround = Physics2D.OverlapCircle (groundChecker.position, groundCheckRadius,groundLayer);
	}
	bool isInputJump() {
		return (Input.GetAxis ("Jump") > 0);
	}
	bool isInputFire1() {
		return (Input.GetAxisRaw("Fire1")>0);
	}
	float inputVertical() {
		return(Input.GetAxis ("Vertical"));
	}
	float inputHorizontal() {
		return (Input.GetAxis ("Horizontal"));
	}
	void jump() {
		playerAnimation.SetBool ("isTouchGround",touchGround); 
		playerBody.AddForce(new Vector2(0,jumpForce));
	}
	void climbLadder() {
		Vector2 titikAwal;
		Vector2 titikAkhir;
		float x;
		float y;
		bool allowUp;
		bool allowDown;
		bool allowHorizontal;
		x = transform.position.x;
		y = transform.position.y;
	}
	void jumpAnimation() {
		playerAnimation.SetBool ("isTouchGround",touchGround);
		playerAnimation.SetFloat ("verticalSpeed", playerBody.velocity.y);
	}
	void moveHorizontal(float horizontalSpeed) {
		playerBody.velocity = new Vector2 (horizontalSpeed*maxSpeed,playerBody.velocity.y);
		playerAnimation.SetFloat("speed",Mathf.Abs (horizontalSpeed));
		if ((horizontalSpeed>0)&&(!facingRight)){
			flipFacing();
		} else if ((horizontalSpeed<0)&&(facingRight)){
			flipFacing();
		}
	}
	/*void firing() {
		if (Time.time > reloadTime) {
			reloadTime = Time.time+fireRate;
			if (facingRight){
				Instantiate(missile,launchPod.position,Quaternion.Euler (new Vector3(0,0,0)));
			} else {
				Instantiate(missile,launchPod.position,Quaternion.Euler (new Vector3(0,0,180)));
			}
		}
	}*/
}
