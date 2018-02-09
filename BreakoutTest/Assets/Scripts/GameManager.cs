using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    //Board Reference, assign in editor
    [SerializeField]
    private Board mBoard;

    //Ball info, assign first ball in editor
    /// <summary>
    /// The ball. New balls will launch from this ball's starting position.
    /// </summary>
    [SerializeField]
    private Ball activeBall;
    Vector3 startingBallPosition; // where to spawn new balls, taken from activeBall's initial position when game starts

    //Paddle info
    /// <summary>
    /// The paddle, assign or it won't shrink on level clear.
    /// </summary>
    [SerializeField]
    private Paddle mPaddle; // paddle, assign in editor

    //UI references, should be assigned through editor
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject gameOverPanel;
    //HUD labels
    [SerializeField]
    private Text livesLabel;
    [SerializeField]
    private Text scoreLabel;
    [SerializeField]
    private Text levelLabel;
    [SerializeField]
    private Text gameOverScoreLabel;


    //Gamestate info
    int livesLeft = 3;
    int currentLevel = 1;
    int score = 0;
    bool inPlay = false;

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
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void StartGame()
    {
        startingBallPosition = activeBall.transform.position;
        activeBall.Launch();
        startButton.SetActive(false);
        UpdateLabels();
        livesLabel.gameObject.SetActive(true);
        levelLabel.gameObject.SetActive(true);
        scoreLabel.gameObject.SetActive(true);
        inPlay = true;
    }

    /// <summary>
    /// Reset ball to starting position and launch downwards with its movespeed unchanged.
    /// </summary>
    public void ResetBall(float delay = 3)
    {
        Debug.Log("ResetBall called");
        activeBall.Stop();
        activeBall.transform.position = startingBallPosition;
        activeBall.LaunchOnDelay(delay);
    }

 
    /// <summary>
    /// Enables post-game UI, sets inPlay to false
    /// </summary>
    void GameOver()
    {
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
            inPlay = false;
        }

    }

    /// <summary>
    /// Resets HUD, Board, Ball, and gamestate variables
    /// </summary>
    public void RestartGame()
    {
        if (inPlay)
        {
            Debug.Log("Already in play, can't restart!");
            return;
        }
        mBoard.Reset();
        gameOverPanel.SetActive(false);
        livesLeft = 3;
        currentLevel = 1;
        score = 0;
        SpawnNewBall();
        mPaddle.UnShrink();
        StartGame();
    }

    /// <summary>
    /// Create a new ball at the spawn point.
    /// </summary>
    void SpawnNewBall()
    {
        GameObject newBall = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"), startingBallPosition, transform.rotation) as GameObject;
        activeBall = newBall.GetComponent<Ball>();
        activeBall.LaunchOnDelay(2);
    }

    public void BallDied()
    {
        livesLeft -= 1;
        UpdateLabels(); 
        if (livesLeft > 0)
        {
            Invoke("SpawnNewBall", 1.0f);
        }
        else
        {
            GameOver();
        }
    }
    
    /// <summary>
    /// Increases score by <b>points</b> and updates HUD
    /// </summary>
    /// <param name="points"></param>
    public void IncreaseScore(int points)
    {
        score += points;
        UpdateLabels();
    }
    public void RowCleared()
    {
        activeBall.IncreaseSpeed();
    }

    /// <summary>
    /// Sets up new level, Resets ball, updates gamestate info, shrinks paddle if first level was just cleared.
    /// </summary>
    public void LevelCleared()
    {
        Debug.Log("Level Cleared");
        //if cleared the first level, shrink the paddle
        if (currentLevel == 1)
        {
            mPaddle.Shrink();
        }
        currentLevel++;
   
        UpdateLabels();
        mBoard.DelayedReset(2); // was having problems disabling then enabling the bricks too quickly;
        ResetBall();
    }

    /// <summary>
    /// Update HUD to reflect gamestate variables
    /// </summary>
    void UpdateLabels()
    {
        if (livesLabel != null)
        {
            livesLabel.text = "Lives: " + livesLeft;
        }
        if (levelLabel != null)
        {
            levelLabel.text = "Level " + currentLevel;
        }
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + score;
        }
        if (gameOverScoreLabel != null)
        {
            gameOverScoreLabel.text = "Score: " + score;
        }
    }

    
}
