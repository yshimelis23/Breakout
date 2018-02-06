using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    int numRows = 8;
    int numColumns = 11;
    

    struct Row
    {
        Brick[] mBrick;
        int mNum;
    }
    Row[] mRows;

	// Use this for initialization
	void Start () {
	    for( int i = 0; i < numColumns; i++)
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


