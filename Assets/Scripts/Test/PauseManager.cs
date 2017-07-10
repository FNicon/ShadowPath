using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    
    public Pause pauseScript;
    public AudioButton sfx;

    // Use this for initialization
    void Start () {
		
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
        GameObject menu, background;
        menu = transform.FindChild("PauseMenu").gameObject;
        background = transform.FindChild("PauseBackground").gameObject;
        menu.active = false;
        background.active = false;
        //unfreeze game objects
        pauseScript.unfreezeObjects();
        //signaling that the game is unpaused
        pauseScript.isPaused = false;
    }

    public void pauseGame()
    {
        //memunculkan menu pause
        GameObject menu, background;
        menu = transform.FindChild("PauseMenu").gameObject;
        background = transform.FindChild("PauseBackground").gameObject;
        menu.active = true;
        background.active = true;
        //freeze game objects
        pauseScript.freezeObjects();
        //signaling that the game is paused
        pauseScript.isPaused = true;
        //playing sfx
        sfx.onCancel();
    }

    bool isInputPause()
    {
        return (Input.GetKeyDown(KeyCode.Escape));
    }
}
