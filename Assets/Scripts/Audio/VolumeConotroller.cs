using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeConotroller : MonoBehaviour {

	private AudioSource audio;

	public int settingVolume;
	public int eventVolume;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		audio.volume = settingVolume * eventVolume;
	}
}
