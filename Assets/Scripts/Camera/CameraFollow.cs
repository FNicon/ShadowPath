using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform follow;
	public float smoothMotion;
	private Vector3 offset;
	public float minimumX;
	public float minimumY;

	// Use this for initialization
	void Start () {
		offset = transform.position - follow.position;
		minimumY = transform.position.y;
	}
	void Update() {
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newCameraPosition;
		newCameraPosition = follow.position + offset;
		transform.position = Vector3.Lerp (transform.position, newCameraPosition, smoothMotion * Time.deltaTime);
		//}
		//if (transform.position.x < minimumX) {
		//	transform.position = new Vector3(minimumX,transform.position.y,transform.position.z);
		//}
		if (transform.position.y < minimumY) {
			transform.position = new Vector3(transform.position.x,minimumY,transform.position.z);
		}
	}
}
