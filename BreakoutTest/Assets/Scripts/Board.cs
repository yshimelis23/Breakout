using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    /// <summary>
    /// Rows of bricks that form board
    /// </summary>
    [SerializeField]
    private RowManager[] mRows;
    int numRowsAlive;

	// Use this for initialization
	void Start () {
        numRowsAlive = mRows.Length;
        foreach (RowManager r in mRows)
        {
            r.mBoard = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Reset rows in board
    /// </summary>
    public void Reset()
    {
        foreach (RowManager r in mRows)
        {
            numRowsAlive++;
            r.ResetRow();
        }
    }

    /// <summary>
    /// Calls Reset() on a t second delay.
    /// </summary>
    public void DelayedReset(float t)
    {
        Invoke("Reset", t);
    }

    /// <summary>
    /// Decrements active row count and calls BoardCleared() if all rows have been cleared
    /// </summary>
    public void RowCleared()
    {
        numRowsAlive--;
        if (numRowsAlive == 0)
        {
            BoardCleared();
        }
    }

    /// <summary>
    /// Notifies GameManager that board has been cleared
    /// </summary>
    void BoardCleared()
    {
        GameManager.instance.LevelCleared();
    }
}


