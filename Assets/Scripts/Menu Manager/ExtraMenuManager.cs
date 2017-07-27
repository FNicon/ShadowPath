using UnityEngine;
using System.Collections;

public class ExtraMenuManager : MonoBehaviour {
	public GameObject mainPanel;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Backbutton() {
		gameObject.SetActive (false);
		mainPanel.SetActive (true);
	}
}
