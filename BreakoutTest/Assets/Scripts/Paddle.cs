using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 5;
    bool hasShrunk = false;
    Vector3 startingScale;

	// Use this for initialization
	void Start () {
        startingScale = transform.localScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * moveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
	}

    /// <summary>
    /// If Shrink() hasn't been called yet, scales paddle to 80%
    /// </summary>
    public void Shrink()
    {
        if (!hasShrunk)
        {
            hasShrunk = true;
            transform.localScale *= .8f;
        }
    }

    /// <summary>
    /// Resets scale to (1,1,1);
    /// </summary>
    public void UnShrink()
    {
        transform.localScale = startingScale;
    }
}
