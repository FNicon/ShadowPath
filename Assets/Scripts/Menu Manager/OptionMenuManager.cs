using UnityEngine;
using System.Collections;

public class OptionMenuManager : MonoBehaviour {
	public GameObject mainPanel;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void backbutton() {
		gameObject.SetActive (false);
		mainPanel.SetActive (true);
	}
}
