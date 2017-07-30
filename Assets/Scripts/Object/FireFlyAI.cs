using UnityEngine;
using System.Collections;

public class FireFlyAI : MonoBehaviour {
    private bool isDecreasing;
    private LightRay lightSource;
    private float maxRadius;
    public float minRadius;
    private float radiusNow;
	public float scaleChange;
	private float target;
    public float waitSeconds;
	public bool isPaused=false;

    // Use this for initialization
    void Start () {
        lightSource = GetComponent<LightRay>();
        maxRadius = lightSource.lightRadius;
        isDecreasing = true;
        target = minRadius;
        radiusNow = maxRadius;
		StartCoroutine ("LightDelay", waitSeconds);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDecreasing && radiusNow <= target) {
			target = maxRadius;
			isDecreasing = !isDecreasing;
		} else if (!isDecreasing && radiusNow >= target) {
			target = minRadius;
			isDecreasing = !isDecreasing;
		}
	}
	IEnumerator LightDelay(float delay) {
		while (true) {
			ChangeLightRadius ();
			yield return new WaitForSeconds (delay);
		}
	}
	void ChangeLightRadius() {
		if (!isPaused) {
			if (!isDecreasing) {
				radiusNow = radiusNow + scaleChange;
			} else {
				radiusNow = radiusNow - scaleChange;
			}
		}
		lightSource.lightRadius = radiusNow;
	}
}
