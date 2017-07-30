using UnityEngine;
using System.Collections;

public class ExitMenuManager : MonoBehaviour {
	public GameObject mainPanel;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void NoButton() {
		gameObject.SetActive (false);
		mainPanel.SetActive (true);
	}
	public void YesButton() {
		if (Application.isEditor) {
			//UnityEditor.EditorApplication.isPlaying = false;
		} else {
			Application.Quit ();
		}
	}
}
