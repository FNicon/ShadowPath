using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	public void StartGame() {
		//int nextLevel;
		SceneManager.LoadScene ("Tutorial Scene");
    }
	public void Exit() {
		exitPanel.SetActive (true);
		gameObject.SetActive (false);
	}
	public void Option() {
		optionPanel.SetActive (true);
		gameObject.SetActive (false);
	}
	public void Extra() {
		extraPanel.SetActive (true);
		gameObject.SetActive (false);
	}
}
