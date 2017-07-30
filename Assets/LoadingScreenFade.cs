using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class LoadingScreenFade : MonoBehaviour {

	public float fadingSpeed;
	public GameObject[] disableThis;

	private bool fadingIn = false;
	private bool fadingOut = false;
	// Use this for initialization
	void Start () {
		FadeOut ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fadingIn) {
			SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer> ();
			Image[] images = gameObject.GetComponentsInChildren<Image> ();
			foreach (SpriteRenderer sprite in sprites) {
				if (sprite.color.a < 1f) {
					Color newColor = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + fadingSpeed);
					sprite.color = newColor;
				}
			}
			foreach (Image image in images) {
				if (image.color.a < 1f) {
					Color newColor = new Color (image.color.r, image.color.g, image.color.b, image.color.a + fadingSpeed);
					image.color = newColor;
				}
			}
		} else if (fadingOut) {
			SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer> ();
			Image[] images = gameObject.GetComponentsInChildren<Image> ();
			foreach (SpriteRenderer sprite in sprites) {
				if (sprite.color.a > 0f) {
					Color newColor = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - fadingSpeed);
					sprite.color = newColor;
				}
			}
			foreach (Image image in images) {
				if (image.color.a > 0f) {
					Color newColor = new Color (image.color.r, image.color.g, image.color.b, image.color.a - fadingSpeed);
					image.color = newColor;
				}
			}
		}
	}

	public void FadeIn(){
		fadingIn = true;
		fadingOut = false;
		DisableObjects();
	}

	public void FadeOut(){
		fadingIn = false;
		fadingOut = true;
	}

	public void Stop(){
		fadingIn = false;
		fadingOut = false;
	}

	void DisableObjects(){
		for (int i = 0; i < disableThis.Length; i++) {
			disableThis [i].SetActive (false);
		}
	}
}
