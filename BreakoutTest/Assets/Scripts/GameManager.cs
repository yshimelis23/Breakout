using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Ball activeBall;
    Vector3 startingBallPosition;

    //UI references, public for easy editor access
    public GameObject StartButton;
    public Text livesLabel;
    public Text scoreLabel;
    public Text levelLabel;

    //Gamestate info
    int livesLeft = 3;
    int currentLevel = 1;
    int score = 0;
    //TODO: add ref to a brick manager of some kind

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        //TODO: launch ball enable/disable UI elements as needed
        startingBallPosition = activeBall.transform.position;
        activeBall.Launch();
        StartButton.SetActive(false);
        UpdateLabels();
        livesLabel.gameObject.SetActive(true);
        levelLabel.gameObject.SetActive(true);
        scoreLabel.gameObject.SetActive(true);
    }

    void GameOver()
    {
        //TODO
    }

    public void BallDied()
    {
        //TODO: dec livesLeft and spawn new Ball
        livesLeft -= 1;
        UpdateLabels(); 
        if (livesLeft > 0)
        {
            //TODO: spawn new ball at startingBallPosition and Launch
            GameObject newBall = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"));
            activeBall = newBall.GetComponent<Ball>();
            //activeBall.transform.position = startingBallPosition;
            activeBall.Launch();
        }
        else
        {
            GameOver();
        }
    }

    void LevelCleared()
    {
        //TODO
    }

    void UpdateLabels()
    {
        livesLabel.text = "Lives: " + livesLeft;
        levelLabel.text = "Level " + currentLevel;
        scoreLabel.text = "Score: " + score;
    }
}
