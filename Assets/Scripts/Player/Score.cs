using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public Text textCounter;
	private int scoreCounter;

	// Use this for initialization
	void Start () {
		scoreCounter = 0;
		updateText();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void updateText() {
		textCounter.text = scoreCounter.ToString();
	}
	public void scorePlus() {
		scoreCounter = scoreCounter + 1;
	}
}
