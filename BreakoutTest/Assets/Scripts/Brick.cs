using UnityEngine;
using System.Collections;

public enum BrickState {Untouched, Weakened, Destroyed};
public enum BrickColor {Yellow, Green, Orange, Red};

public class Brick : MonoBehaviour {
    public RowManager mRowManager;
    [SerializeField]
    private int mValue = 1;

    BrickState mState = BrickState.Untouched;



	// Use this for initialization
	void Start () {

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

    public void SetColor(BrickColor color)
    {
        switch (color)
        {
            case BrickColor.Yellow:
                mValue = 1;
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case BrickColor.Green:
                mValue = 3;
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case BrickColor.Orange:
                mValue = 5;
                GetComponent<Renderer>().material.color = new Color(1, 165f / 255f, 0);
                break;
            case BrickColor.Red:
                mValue = 7;
                GetComponent<Renderer>().material.color = Color.red;
                break;
            default:
                mValue = 1;
                GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }

    private void WeakenBrick()
    {
        mState = BrickState.Weakened;
        //TODO: handle any sound effects

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
        else
        {
            Debug.Log("Brick missing reference to RowManager");
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

    /// <summary>
    /// Set State to Untouched and Undo any other effects
    /// </summary>
    public void Reset()
    {
        //Debug.Log("In reset");
        mState = BrickState.Untouched;
        gameObject.SetActive(true);
    }
}
