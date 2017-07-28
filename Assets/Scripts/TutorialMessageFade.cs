using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageFade : MonoBehaviour {

	public GameObject player;

	private int maximumDistance = 10;

	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float deltax, deltay, distance,newAlpha;
		deltax = Mathf.Abs(player.transform.position.x - transform.position.x);
		deltay = Mathf.Abs(player.transform.position.y - transform.position.y);
		distance = Mathf.Sqrt (deltax * deltax + deltay * deltay);
		newAlpha = Mathf.Max(0,((maximumDistance - distance)/maximumDistance));
		sprite.color = new Color (1f, 1f, 1f, newAlpha);
	}
}
