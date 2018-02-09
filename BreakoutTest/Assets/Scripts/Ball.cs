using UnityEngine;
using System.Collections;
using System;

public class Ball : MonoBehaviour {

    private float moveSpeed = 2.5f; /// <summary>
    /// Current speed of the ball, or initial speed if it hasn't launched.
    /// </summary>
    [SerializeField]
    private float speedIncreaseOnClear = .5f; /// <summary>
    /// Increase in speed when a row is cleared
    /// </summary>

    bool hasLaunched = false;

    public bool inPlay
    {
        get
        {
            return hasLaunched;
        }
        set
        {
            hasLaunched = inPlay;
        }
    }

    Rigidbody2D mRigidBody2D;
    // Use this for initialization
    void Start () {
        mRigidBody2D = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (inPlay)
        {
            mRigidBody2D.velocity = mRigidBody2D.velocity.normalized * moveSpeed; //maintain constant speed
        }

        //Debug.Log(mRigidBody2D.velocity);
        //Debug.Log("Speed is: " + mRigidBody2D.velocity.magnitude);
    }

    /// <summary>
    /// Stops the ball
    /// </summary>
    public void Stop()
    {
        inPlay = false;
        hasLaunched = false;
        mRigidBody2D.velocity = Vector2.zero;
    }
    /// <summary>
    /// Launches Ball if it isn't already in play
    /// </summary>
    public void Launch()
    {
        if (!hasLaunched)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * moveSpeed), ForceMode2D.Impulse);
            hasLaunched = true;
        }
    }

    /// <summary>
    /// increments speed by member variable <b>speedIncreaseOnClear</b>
    /// </summary>
    public void IncreaseSpeed()
    {
        moveSpeed += speedIncreaseOnClear;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        /* Pseudocode
         * If hit Brick, Brick will handle its own side of things
         * If hit Paddle, calculate new angle based on dist from paddle center 
         * Else, reflect angle
         * Note: handle collision with floor in OnTriggerEnter2D()
         */
        if (coll.gameObject.GetComponent<Paddle>()) // handle paddle collison
        {
            Vector2 newDir = (transform.position - coll.transform.position).normalized;
            mRigidBody2D.velocity = moveSpeed * newDir;
        }
        else // reflect the angle across the normal of the other surface
        {
            Vector2 normal = coll.contacts[0].normal;
            mRigidBody2D.AddForce(Vector2.Reflect(coll.relativeVelocity.normalized, normal).normalized * moveSpeed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// calls Launch() after t seconds
    /// </summary>
    /// <param name="t"></param>
    public void LaunchOnDelay(float t)
    {
        Invoke("Launch", t);
    }

    void OnCollisionExit2D(Collision2D coll) // TODO: remove this when I'm done
    {
        if (coll.gameObject.GetComponent<Paddle>())
        {
            Vector2 newDir = (transform.position - coll.transform.position).normalized;
            if (newDir.y < 0) //don't let ball bounce downwards from paddle?
            {
                newDir.y *= -1;
            }
            mRigidBody2D.velocity = moveSpeed * newDir;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            //inform gamemanager that ball hit killzone, then destroy
            GameManager.instance.BallDied();
            Destroy(gameObject);
        }
    }

}
