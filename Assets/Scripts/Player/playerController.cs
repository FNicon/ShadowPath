using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	private shadowWalk shadowScript;
	private GroundMovement groundScript;
	private LadderMovement ladderScript;
	public Death deathScript;
	//public playerRopeMovement ropeScript;
	private PushPullMovement pushPullScript;
	private Score counter;
	public float hurtForce;
	private Interact interactObject;
    private Animator playerAnimation;
	private Rigidbody2D playerBody;
    private bool isDeath;
	public bool immovable = false;
	//private Transform pullObjectPosition;
	public bool ping = false;

    // Use this for initialization
    void Start () {
        isDeath = false;
		counter = GetComponent<Score> ();
		shadowScript = GetComponent<shadowWalk> ();
		groundScript = GetComponent<GroundMovement> ();
		ladderScript = GetComponent<LadderMovement> ();
		pushPullScript = GetComponent<PushPullMovement> ();
		playerBody = GetComponent<Rigidbody2D> ();
        playerAnimation = GetComponent<Animator>();
    }
	void Update(){
        if (!isDeath)
        {
            if (isInputJump()) {
				if (pushPullScript.isPullingObject ()) {
					pushPullScript.releaseObject ();
				}
				if (groundScript.GetTouchGround ()) {
					groundScript.Jump ();
				} //else if (ropeScript.isClimbRope()) {
					//ropeScript.JumpOff();
				//}
            }
            if (isInputFire1())
            {
				ping = shadowScript.isActive;
				if (!shadowScript.isActive) {
					shadowScript.activate ();
				} else {
					shadowScript.deactivate ();
					//firing();
				}
            }
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float horizontalSpeed;
        float verticalSpeed;
        if (!isDeath) {
			if (!immovable) {
				horizontalSpeed = inputHorizontal();
				verticalSpeed = inputVertical ();
				//if (ropeScript.isClimbRope ()) {
					//ropeScript.MovementOnRope (horizontalSpeed, verticalSpeed);
				/*} else*/ if (pushPullScript.isPullingObject ()) {
					pushPullScript.MoveHorizontal (horizontalSpeed);
				} else {
					groundScript.MoveHorizontal (horizontalSpeed);
				}
			}
		}
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
			playerBody.velocity = new Vector2(playerBody.velocity.x - other.GetComponent<Rigidbody2D>().velocity.x,playerBody.velocity.y + 10.0f);
			playerDestroy();
			deathScript.die ();
		}
    }

	bool isInputJump() {
		return (Input.GetAxis ("Jump") > 0 && !immovable);
	}
	bool isInputFire1() {
		//return (Input.GetAxisRaw("Fire1")>0 && !immovable);
		return (Input.GetButtonDown("Fire1") && !immovable);
	}
	float inputVertical() {
		if (immovable) {
			return 0;
		} else {
			return(Input.GetAxis ("Vertical"));
		}
	}
	float inputHorizontal() {
		if (immovable) {
			return 0;
		} else {
			return (Input.GetAxis ("Horizontal"));
		}
	}
    public void playerDestroy() {
        //ganti sama gameOverAnimation
        playerAnimation.SetBool("isDeath", true);
        //Destroy(GetComponent<BoxCollider2D>());
        //Destroy(gameObject);
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
	public void RevivePlayer() {
		immovable = false;
		isDeath = false;
	}
}
