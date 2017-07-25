using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    
    public Pause pauseScript;
	public GameObject pausePanel;
	public GameObject pauseBackground;
    public AudioButton sfx;

    // Use this for initialization
    void Start () {
		pausePanel.SetActive (false);
		pauseBackground.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
        if (isInputPause())
        {
			
            if (pauseScript.isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
	}

    public void resumeGame()
    {
        //menghilangkan menu pause
		pausePanel.SetActive (false);
		pauseBackground.SetActive (false);
        //unfreeze game objects
        pauseScript.unfreezeObjects();
        //signaling that the game is unpaused
        //pauseScript.isPaused = false;
    }

    public void pauseGame()
    {
        //memunculkan menu pause
		pausePanel.SetActive (true);
		pauseBackground.SetActive (true);
        //freeze game objects
        pauseScript.freezeObjects();
        //signaling that the game is paused
        //pauseScript.isPaused = true;
        //playing sfx
        //sfx.onCancel();
    }

    bool isInputPause()
    {
        return (Input.GetKeyDown(KeyCode.Escape));
    }
}
