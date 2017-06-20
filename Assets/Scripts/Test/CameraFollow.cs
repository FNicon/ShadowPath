using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform follow;
	public float smoothMotion;
	Vector3 offset;
	float minimumY;

	// Use this for initialization
	void Start () {
		offset = transform.position - follow.position;
		minimumY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newCameraPosition;
		newCameraPosition = follow.position + offset;
		transform.position = Vector3.Lerp (transform.position, newCameraPosition,smoothMotion*Time.deltaTime);
		if (transform.position.y < minimumY) {
			transform.position = new Vector3(transform.position.x,minimumY,transform.position.z);
		}
	}
}
