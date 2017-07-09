using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerController : MonoBehaviour {
	public GroundMovement groundScript;
	public ladderMovement ladderScript;
	//Counter
	public Score counter;
	//Player
	//private Rigidbody2D playerBody;
	//private Animator playerAnimation;
	//private bool facingRight;

	private Interact interactObject;
	//public Transform launchPod;
	//public GameObject missile;
	//float fireRate = 0.5f;
	//float reloadTime = 0f;

	// Use this for initialization
	void Start () {
		//playerBody = GetComponent<Rigidbody2D>();
		//playerAnimation = GetComponent<Animator>();
	}
	void Update(){
		if(isInputJump ()) {
			if(groundScript.getTouchGround()) {
				groundScript.jump ();
			}
		}
		if  (isInputFire1()){
			//firing();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float horizontalSpeed;
		//float verticalSpeed;
		horizontalSpeed = inputHorizontal ();
		//verticalSpeed = inputVertical ();

		groundScript.moveHorizontal (horizontalSpeed);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			counter.scorePlus();
			counter.updateText ();
		} else if (other.gameObject.CompareTag ("Interact")) {
			if (isInputFire1 ()) {
				interactObject = other.gameObject.GetComponent<Interact>();
				interactObject.generateText();
			}
		} else if (other.gameObject.CompareTag ("Rope")) {

		} else if (other.gameObject.CompareTag ("Enemy")) {
			Destroy(gameObject);
		}
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
