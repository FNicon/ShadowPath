﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interact : MonoBehaviour {
	public string show;
	private Text recent;

	// Use this for initialization
	void Start () {
		recent = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void generateText() {
		recent.text = show;
	}
}