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
	public void OnHover() {
		source.PlayOneShot (sfxHover);
	}
	public void OnClick() {
		source.PlayOneShot (sfxClick);
	}
	public void OnCancel() {
		source.PlayOneShot (sfxCancel);
	}
}
