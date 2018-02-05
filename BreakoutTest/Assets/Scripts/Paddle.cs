using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public float moveSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Transform>().position -= new Vector3(moveSpeed, 0 , 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Transform>().position += new Vector3(moveSpeed, 0, 0);
        }
	}
}
