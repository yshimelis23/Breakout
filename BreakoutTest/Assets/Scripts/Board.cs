using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    [SerializeField]
    private RowManager[] mRows; // assign rows in editor
    int numRowsAlive;

	// Use this for initialization
	void Start () {
        numRowsAlive = mRows.Length;
        foreach (RowManager r in mRows)
        {
            r.mBoard = this; //TODO: fix this lol
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        Debug.Log("In RowManager.Reset()");
        foreach (RowManager r in mRows)
        {
            numRowsAlive++;
            r.ResetRow();
        }
    }

    public void RowCleared()
    {
        Debug.Log("Row Cleared");
        numRowsAlive--;
        if (numRowsAlive == 0)
        {
            BoardCleared();
        }
    }

    void BoardCleared()
    {
        //TODO: implement level change
        Debug.Log("Board Cleared");
        GameManager.instance.LevelCleared();
    }
}


