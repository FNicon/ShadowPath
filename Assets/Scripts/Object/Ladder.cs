using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	[Header("Allow Fall in Direction")]
	public bool allowFallBottom;
	public bool allowFallTop;
	public bool allowFallLeft;
	public bool allowFallRight;
	[Header("Allow Movement")]
	public bool allowMoveLeft;
	public bool allowMoveRight;
	public bool allowMoveDown;
	public bool allowMoveUp;
	public bool allowSnapToMiddle;
	[Header("Rope/Ladder")]
	public bool isLadder;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
