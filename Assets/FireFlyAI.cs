using UnityEngine;
using System.Collections;

public class FireFlyAI : MonoBehaviour {
	private LightRay lightSource;
	private float radiusNow;
	private float maxRadius;
	public float minRadius;
	public float waitSeconds;
	public float scaleChange;
	private float target;
	private bool isDecreasing;

	public bool movingDown;
	public float movementMinTarget;
	public float movementMaxTarget;
	public float currentTarget;
	public float verticalSpeed;
	private float speed;
	private Rigidbody2D body;
	private float decreaseSpeed = 0;

	// Use this for initialization
	void Start () {
		isDecreasing = true;
		movingDown = true;
		body = GetComponent<Rigidbody2D> ();
		speed = -1f;
		currentTarget = movementMinTarget;
		lightSource = GetComponent<LightRay> ();
		maxRadius = lightSource.lightRadius;
		radiusNow = maxRadius;
		StartCoroutine ("lightDelay", waitSeconds);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDecreasing && radiusNow <= target) {
			target = maxRadius;
			isDecreasing = !isDecreasing;
		} else if (!isDecreasing && radiusNow >= target) {
			target = minRadius;
			isDecreasing = !isDecreasing;
		}
		//changeLightRadius ();	
	}
	void FixedUpdate() {
		if (movingDown && transform.position.y < currentTarget) {
			speed = speed * -1;
			currentTarget = movementMaxTarget;
			movingDown = false;
			decreaseSpeed = 0.2f;
		} else if (!movingDown && transform.position.y > currentTarget) {
			speed = speed * -1;
			currentTarget = movementMinTarget;
			movingDown = true;
			decreaseSpeed = 0.2f;
		} else {

		}
		body.velocity = new Vector2 (0, (speed * verticalSpeed) + (speed * decreaseSpeed));
	}
	IEnumerator lightDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			changeLightRadius ();
			decreaseSpeed = decreaseSpeed + 0.1f;
		}
	}
	void changeLightRadius() {
		if (!isDecreasing) {
			radiusNow = radiusNow + scaleChange;
		} else {
			radiusNow = radiusNow - scaleChange;
		}
		lightSource.lightRadius = radiusNow;
	}
}
