using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AI : MonoBehaviour {
    public bool movingRight;
    public bool facingRight;
    public float rightTarget;
    public float leftTarget;
    private float currentTarget;
    public float maxSpeed;
    private float currentSpeed;
    private float negator;
    private Rigidbody2D body;

    // Use this for initialization
    void Start () {
        movingRight = true;
        body = GetComponent<Rigidbody2D>();
        negator = 1f;
        currentTarget = rightTarget;
        currentSpeed = maxSpeed;
        facingRight = true;
    }
	
	// Update is called once per frame
	void Update () {

    }
    void FixedUpdate()
    {
        if (movingRight && transform.position.x > currentTarget)
        {
            negator = negator * -1;
            currentTarget = leftTarget;
            movingRight = false;
            FlipFacing();
            currentSpeed = maxSpeed;
        }
        else if (!movingRight && transform.position.x < currentTarget)
        {
            negator = negator * -1;
            currentTarget = rightTarget;
            movingRight = true;
            FlipFacing();
            currentSpeed = maxSpeed;
        }
        else
        {

        }
        body.velocity = new Vector2(negator * currentSpeed, body.velocity.y);
    }
    public void FlipFacing()
    {
        Vector3 newVector;

        facingRight = !facingRight;
        newVector = transform.localScale;
        newVector.x = newVector.x * -1;
        transform.localScale = newVector;
    }
}
