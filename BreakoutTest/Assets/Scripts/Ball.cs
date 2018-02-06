using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1.0f; /// <summary>
    /// Current speed of the ball, or initial speed if it hasn't launched.
    /// </summary>
    [SerializeField]
    private float speedIncreaseOnClear = .5f; /// <summary>
    /// Increase in speed when a row is cleared
    /// </summary>

    bool hasLaunched = false;

    Rigidbody2D mRigidBody2D;
    // Use this for initialization
    void Start () {
        mRigidBody2D = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {
        mRigidBody2D.velocity = mRigidBody2D.velocity.normalized * moveSpeed;
	}

    public void Launch()
    {
        if (!hasLaunched)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * moveSpeed), ForceMode2D.Impulse);
            hasLaunched = true;
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        /* Pseudocode
         * If hit Brick, Brick will handle its own side of things
         * If hit Paddle, calculate new angle based on dist from paddle center 
         * Else, reflect angle
         * Note: handle collision with floor in OnTriggerEnter2D()
         */
        Debug.Log("Ball Collision: ");
        Debug.Log("Contact at " + coll.contacts[0].point);
        Debug.Log("My Position: " + transform.position);
        Debug.Log("Colliding object position: " + coll.transform.position);
        if (coll.gameObject.GetComponent<Paddle>()) // handle paddle collison
        {
            //TODO: calculate angle based on dist from center of paddle
            
            //Temprorary paddle angle code:
            Vector2 newDir = (transform.position - coll.transform.position).normalized;
            mRigidBody2D.velocity = moveSpeed * newDir;
        }
        else // reflect the angle across the normal of the other surface
        {
            Vector2 normal = coll.contacts[0].normal;
            //Debug.Log("relativeVel is " + coll.relativeVelocity);
            mRigidBody2D.AddForce(Vector2.Reflect(coll.relativeVelocity.normalized, normal).normalized * moveSpeed, ForceMode2D.Impulse);
            Debug.Log("Speed is: " + mRigidBody2D.velocity.magnitude);
            //Debug.Log("New: " + mRigidBody2D.velocity);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
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
            // TODO: inform GameManager that ball died, then destroy self
            GameManager.instance.BallDied();
            Destroy(gameObject);
        }
    }

}
