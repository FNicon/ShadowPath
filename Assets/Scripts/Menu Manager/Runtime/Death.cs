using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {

    public playerController player;
    public GameObject deathScreen;
	public GameObject fadeScreen;
    private bool deathCondition = false;


    // Use this for initialization
    void Start () {
		deathScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void die()
    {
        setDeath(true);
		deathScreen.SetActive (true);
    }

    public bool isDead()
    {
        return deathCondition;
    }

    public void setDeath(bool _deathCondition)
    {
        deathCondition = _deathCondition;
        if (deathCondition)
        {
            player.immovable = true;
        }
    }
	public void deathTransition() {
		fadeScreen.GetComponent<Image> ().CrossFadeAlpha (1, 1, true);
	}
}
