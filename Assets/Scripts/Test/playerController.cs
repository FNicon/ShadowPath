using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerController : MonoBehaviour {
	public float maxSpeed;
	Rigidbody2D playerBody;
	Animator playerAnimation;
	bool facingRight;
	bool touchGround = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundChecker;
	public float jumpForce;
	public Text textcounter;
	public Text interact;
	private int soulCounter;
	bool isOnLadder = false;
	public float stairSpeed;
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
	}

	void Update(){
		if ((touchGround) && (Input.GetAxis ("Jump") > 0)) {
			touchGround = false;
			playerAnimation.SetBool ("isTouchGround",touchGround); 
			playerBody.AddForce(new Vector2(0,jumpForce));
		}
		if (Input.GetAxisRaw("Fire1")>0) {
			//firing();
		}
		float verticalSpeed;
		if (isOnLadder == true) {
			verticalSpeed = Input.GetAxis ("Vertical");
			playerBody.velocity = new Vector2 (playerBody.velocity.x,verticalSpeed*stairSpeed);
			isOnLadder = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		touchGround = Physics2D.OverlapCircle (groundChecker.position, groundCheckRadius,groundLayer);
		playerAnimation.SetBool ("isTouchGround",touchGround);
		playerAnimation.SetFloat ("verticalSpeed", playerBody.velocity.y);

		float horizontalSpeed;
		horizontalSpeed = Input.GetAxis ("Horizontal");
		playerBody.velocity = new Vector2 (horizontalSpeed*maxSpeed,playerBody.velocity.y);
		playerAnimation.SetFloat("speed",Mathf.Abs (horizontalSpeed));
		if ((horizontalSpeed>0)&&(!facingRight)){
			flipFacing();
		} else if ((horizontalSpeed<0)&&(facingRight)){
			flipFacing();
		}
	}
	void flipFacing() {
		facingRight = !facingRight;
		Vector3 newVector;
		newVector = transform.localScale;
		newVector.x = newVector.x * -1;
		transform.localScale = newVector;
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			soulCounter = soulCounter + 1;
			setText ();
		} else if (other.gameObject.CompareTag ("Interact")) {
			if (Input.GetAxisRaw ("Fire1") > 0) {
				generateText ();
			}
		} else if (other.gameObject.CompareTag ("Ladder")) {
			isOnLadder = true;
		}
	}
	void setText() {
		textcounter.text = soulCounter.ToString();
	}
	void generateText() {
		interact.text = soulCounter.ToString();
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
