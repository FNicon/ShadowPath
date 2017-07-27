using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public playerController player;

    private List<GameObject> dynamicObjects = new List<GameObject>();
    private List<GameObject> kinematicObjects = new List<GameObject>();
    private List<GameObject> staticObjects = new List<GameObject>();
    private List<GameObject> animatedObjects = new List<GameObject>();
    private List<Vector2> objectsVelocity = new List<Vector2>();
    private List<float> objectsAngularV = new List<float>();

    public bool isPaused = false;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //freeze all objects in the scene
    public void freezeObjects()
    {
        //Initialization
        dynamicObjects.Clear();
        kinematicObjects.Clear();
        staticObjects.Clear();
        animatedObjects.Clear();
        objectsVelocity.Clear();
        
        //Listing all rigidbodytype and animated objects
        GameObject[] allObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (GameObject singleObject in allObjects)
        {
            if (singleObject.activeInHierarchy && singleObject.hideFlags == HideFlags.None)
            {
                if (singleObject.GetComponent<Rigidbody2D>() != null)
                {
                    Rigidbody2D body;
                    body = singleObject.transform.GetComponent<Rigidbody2D>();
                    if (body.bodyType == RigidbodyType2D.Dynamic)
                    {
                        dynamicObjects.Add(singleObject);
                    }
                    else if (body.bodyType == RigidbodyType2D.Kinematic)
                    {
                        kinematicObjects.Add(singleObject);
                    }
                    else
                    {
                        staticObjects.Add(singleObject);
                    }
                }
                if (singleObject.GetComponent<Animator>() != null)
                {
                    animatedObjects.Add(singleObject);
                }
            }
        }

        //Freezing objects with rigidbodytype = dynamic. saving velocity and angular velocity
        foreach (GameObject singleObject in dynamicObjects)
        {
            Rigidbody2D body;
            Vector2 savedVelocity = new Vector2();
            float savedAngularV = new float();
            body = singleObject.transform.GetComponent<Rigidbody2D>();
            savedVelocity.x = body.velocity.x;
            savedVelocity.y = body.velocity.y;
            savedAngularV = body.angularVelocity;
            objectsVelocity.Add(savedVelocity);
            objectsAngularV.Add(savedAngularV);
            body.bodyType = RigidbodyType2D.Static;
        }

        //Freezing objects with rigidbodytype = kinematic. saving velocity and angular velocity
        foreach (GameObject singleObject in kinematicObjects)
        {
            Rigidbody2D body;
            Vector2 savedVelocity = new Vector2();
            float savedAngularV = new float();
            body = singleObject.transform.GetComponent<Rigidbody2D>();
            savedVelocity.x = body.velocity.x;
            savedVelocity.y = body.velocity.y;
            savedAngularV = body.angularVelocity;
            objectsVelocity.Add(savedVelocity);
            objectsAngularV.Add(savedAngularV);
            body.bodyType = RigidbodyType2D.Static;
        }

        //Freezing all animated objects
        foreach(GameObject singleObject in animatedObjects)
        {
            Animator animation;
            animation = singleObject.transform.GetComponent<Animator>();
            animation.enabled = false;
        }
		isPaused = true;
		player.immovable = true;
    }

    //unfreeze all objects in the scene
    public void unfreezeObjects()
    {
        //unfreezing all dynamic objects. returning the velocity and angular velocity
        foreach (GameObject singleObject in dynamicObjects)
        {
            Rigidbody2D body;
            body = singleObject.transform.GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Dynamic;
            body.velocity = new Vector2(objectsVelocity[0].x,objectsVelocity[0].y);
            objectsVelocity.RemoveAt(0);
            body.angularVelocity = objectsAngularV[0];
            objectsAngularV.RemoveAt(0);
        }

        //unfreezing all kinematic objects. returning the velocity and angular velocity
        foreach (GameObject singleObject in kinematicObjects)
        {
            Rigidbody2D body;
            body = singleObject.transform.GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.velocity = new Vector2(objectsVelocity[0].x, objectsVelocity[0].y);
            objectsVelocity.RemoveAt(0);
            body.angularVelocity = objectsAngularV[0];
            objectsAngularV.RemoveAt(0);
        }

        //unfreezing all animated objects
        foreach(GameObject singleObject in animatedObjects)
        {
            Animator animation;
            animation = singleObject.transform.GetComponent<Animator>();
            animation.enabled = true;
        }
		player.immovable = false;
		isPaused = false;
    }
}
