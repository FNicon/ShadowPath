using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public playerController player;
    public GameObject deathScreen;
    private bool deathCond = false;

    public bool ping1 = false;
    public bool ping2 = false;
    public bool ping3 = false;


    // Use this for initialization
    void Start () {
        ping1 = true;
	}
	
	// Update is called once per frame
	void Update () {
        ping2 = true;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        ping3 = true;
        if (other.gameObject.CompareTag("Enemy"))
        {
            die();
        }
    }

    public void die()
    {
        setDeath(true);
        deathScreen.active = true;
    }

    public bool isDead()
    {
        return deathCond;
    }

    public void setDeath(bool _deathCond)
    {
        deathCond = _deathCond;
        if (deathCond)
        {
            player.immovable = true;
        }
    }
}
