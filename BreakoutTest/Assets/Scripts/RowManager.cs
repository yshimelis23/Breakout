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

    public void ResetRow()
    {
        foreach (Brick b in mBricks)
        {
            bricksActive++;
            b.Reset();
            b.SetColor(mColor);
        }
    }

    public void BrickDestroyed()
    {
        bricksActive--;
        if (bricksActive == 0)
        {
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        //TODO: Notify manager + board the row has been destroyed
        //TODO: play any visual/audio effects
        GameManager.instance.RowCleared();
        mBoard.RowCleared();
    }
}
