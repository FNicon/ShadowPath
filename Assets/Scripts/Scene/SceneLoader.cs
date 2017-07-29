using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public string sceneToLoad;
	private string currentScene;
	private string mainMenuScene;
	public BoxCollider2D boxTrigger;

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene ().name;
		mainMenuScene = "Main Menu";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void nextScene () {
		SceneManager.LoadScene (sceneToLoad);
	}
	void resetScene() {
		SceneManager.LoadScene (currentScene);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			nextScene ();
		}
	}
	public void menuScene() {
		SceneManager.LoadScene (mainMenuScene);
	}


}
