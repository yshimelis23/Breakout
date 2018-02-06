using UnityEngine;
using System.Collections;

public enum BrickState {Untouched, Weakened, Destroyed};

public class Brick : MonoBehaviour {
    [SerializeField]
    private RowManager mRowManager;
    [SerializeField]
    private int mValue = 1;

    BrickState mState = BrickState.Untouched;



	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.cyan;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeHit()
    {
        switch (mState)
        {
            case BrickState.Untouched:
                WeakenBrick();
                break;
            case BrickState.Weakened:
                DestroyBrick();
                break;
            default:
                break;
        }
    }

    private void WeakenBrick()
    {
        mState = BrickState.Weakened;
        //TODO: handle any effects

        //placeholder weakening effects
        Color newColor = GetComponent<Renderer>().material.color;
        float opacity = newColor.a;
        newColor *= .5f;
        newColor.a = opacity; //don't change alpha
        GetComponent<Renderer>().material.color = newColor;
    }

    private void DestroyBrick()
    {
        //TODO: handle any behaviors on destruction/send any messages to a manager that does that
        //TOOD: play Destruction effects

        mState = BrickState.Destroyed;
        if (mRowManager)
        {
            mRowManager.BrickDestroyed();
        }
        GameManager.instance.IncreaseScore(mValue);
        gameObject.SetActive(false); // disable object rather than destroy it, because it'll be reused for future levels.
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Ball>()) //if hit by a ball, call TakeHit()
        {
            TakeHit();
        }
    }
}
