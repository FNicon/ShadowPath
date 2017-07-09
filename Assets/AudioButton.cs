using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour {
	public AudioSource source;
	public AudioClip sfxHover;
	public AudioClip sfxClick;
	public AudioClip sfxCancel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	public void onHover() {
		source.PlayOneShot (sfxHover);
	}
	public void onClick() {
		source.PlayOneShot (sfxClick);
	}
	public void onCancel() {
		source.PlayOneShot (sfxCancel);
	}
}
