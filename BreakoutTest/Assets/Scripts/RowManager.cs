using UnityEngine;
using System.Collections;

public class RowManager : MonoBehaviour {

    [SerializeField]
    private Brick[] mBricks;
    [SerializeField]
    private Board mBoard;

    int bricksActive;

	// Use this for initialization
	void Start () {
        bricksActive = mBricks.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
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
        //TODO: Notify board the row has been destroyed
        //TODO: play any visual effects
        //TODO: Notify GameManager that row has been destroyed
    }
}
