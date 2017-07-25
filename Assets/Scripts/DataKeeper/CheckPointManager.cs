using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {
	public GameObject player;
	public Transform checkPointPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void onCheckPoint(Transform positionInput) {
		checkPointPosition = positionInput;
	}
	public void onGameOver() {
		player.GetComponent<playerController> ().RevivePlayer();
		player.transform.position = checkPointPosition.position;
	}
}
