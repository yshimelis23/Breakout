using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float moveSpeed = 1.0f;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1* moveSpeed), ForceMode2D.Impulse);
        Debug.Log("In Ball.Start(), should have applied force");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        /* Pseudocode
         * If hit Brick, call the Brick's collisionWithBall
         * If hit Paddle, calculate new angle based on dist from paddle center 
         * Else, reflect angle
         * Note: handle collision with floor in OnTriggerEnter2D()
         */
        if (coll.gameObject.GetComponent<Brick>()) // if wall/ceiling, reflect angle
        {
            //TODO: handle brick ticking
        }
        if (coll.gameObject.GetComponent<Paddle>())
        {
            //TODO: calculate angle based on dist from center of paddle
        }
        else
        {
            //TODO: reflect ball
        } 
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

    }
}
