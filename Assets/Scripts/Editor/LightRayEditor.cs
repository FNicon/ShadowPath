using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LightRay))]
public class LightRayEditor : Editor {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void onSceneGUI() {
		LightRay light;
		light = (LightRay)target;
		Handles.color = Color.red;
		//Handles.DrawWireArc (light.transform.position, Vector2.up, Vector3.forward, 360, light.lightRadius);
		Handles.DrawWireArc (light.transform.position, Vector3.right, Vector3.forward, 360, light.lightRadius);
		Vector3 lightAngleA = light.DirectionFromAngle (-light.lightAngle / 2, false);
		Vector3 lightAngleB = light.DirectionFromAngle (light.lightAngle / 2, false);

		Handles.DrawLine (light.transform.position, light.transform.position + lightAngleA * light.lightRadius);
		Handles.DrawLine (light.transform.position, light.transform.position + lightAngleB * light.lightRadius);
	}
}
