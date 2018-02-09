using UnityEngine;
using System.Collections;

public class RowManager : MonoBehaviour {

    [SerializeField]
    private Brick[] mBricks;
    [SerializeField]
    private BrickColor mColor;
    int bricksActive;

    public Board mBoard;

    // Use this for initialization
    void Start() {
        bricksActive = mBricks.Length;
        foreach (Brick brick in mBricks)
        {
            brick.SetColor(mColor);
            brick.mRowManager = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Debug:
	    if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (Brick b in mBricks)
            {
                b.TakeHit();
            }
        }
	}

    /// <summary>
    /// Reset members of mBricks
    /// </summary>
    public void ResetRow()
    {
        foreach (Brick b in mBricks)
        {
            bricksActive++;
            b.Reset();
            b.SetColor(mColor);
        }
    }

    /// <summary>
    /// Decrements count of live bricks and calls OnRoyCLeared() if none left
    /// </summary>
    public void BrickDestroyed()
    {
        bricksActive--;
        if (bricksActive == 0)
        {
            OnRowCleared();
        }
    }

    /// <summary>
    /// Notify GameManager and Board that a row has been cleared
    /// </summary>
    private void OnRowCleared()
    {
        //Notify manager + board the row has been destroyed
        GameManager.instance.RowCleared();
        mBoard.RowCleared();
    }
}
