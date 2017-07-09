using UnityEngine;
using System.Collections;

public class FireFlyAI : MonoBehaviour {
	private LightRay lightSource;
	private float i;
	public float maxRadius;
	public float minRadius;
	private float target;
	private bool isDecreasing;

	// Use this for initialization
	void Start () {
		isDecreasing = true;
		lightSource = GetComponent<LightRay> ();
		i = lightSource.lightRadius;
		maxRadius = i;
		StartCoroutine ("lightDelay", 0.12f);
	}
	
	// Update is called once per frame
	void Update () {
		//changeLightRadius ();	
	}
	IEnumerator lightDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			changeLightRadius ();
		}
	}
	void changeLightRadius() {
		if (!isDecreasing) {
			i = i + 1;
		} else {
			i = i - 1;
		}
		lightSource.lightRadius = i;
	}
}
