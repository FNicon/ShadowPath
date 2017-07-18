﻿using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	public GameObject optionPanel;
	public GameObject extraPanel;
	public GameObject exitPanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void startgame() {
		int nextLevel;
		nextLevel = Application.loadedLevel + 1;
		Application.LoadLevel (nextLevel);
	}
	public void exit() {
		exitPanel.SetActive (true);
		gameObject.SetActive (false);
	}
	public void option() {
		optionPanel.SetActive (true);
		gameObject.SetActive (false);
	}
	public void extra() {
		extraPanel.SetActive (true);
		gameObject.SetActive (false);
	}
}