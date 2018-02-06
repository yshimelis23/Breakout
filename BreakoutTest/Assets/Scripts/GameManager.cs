using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    //Ball info
    [SerializeField]
    private Ball activeBall;
    Vector3 startingBallPosition;

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

    //Gamestate info
    int livesLeft = 3;
    int currentLevel = 1;
    int score = 0;
    bool inPlay = false;
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
        if (inPlay)
        {
            Debug.Log("Already playing!");
            return;
        }
        startingBallPosition = activeBall.transform.position;
        activeBall.Launch();
        startButton.SetActive(false);
        UpdateLabels();
        livesLabel.gameObject.SetActive(true);
        levelLabel.gameObject.SetActive(true);
        scoreLabel.gameObject.SetActive(true);
        inPlay = true;
    }

    void GameOver()
    {
        //TODO: enable gameover UI element
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
        //TODO: reset board
        if (inPlay)
        {
            Debug.Log("Already in play, can't restart!");
            return;
        }
        gameOverPanel.SetActive(false);
        livesLeft = 3;
        currentLevel = 1;
        score = 0;
        SpawnNewBall();
        StartGame();
    }

    void SpawnNewBall()
    {
        //TODO: spawn new ball at startingBallPosition and Launch
        GameObject newBall = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"), startingBallPosition, transform.rotation) as GameObject;
        activeBall = newBall.GetComponent<Ball>();
        //activeBall.transform.position = startingBallPosition;
        activeBall.Launch();
    }

    public void BallDied()
    {
        //TODO: dec livesLeft and spawn new Ball
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

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateLabels();
    }

    void LevelCleared()
    {
        //TODO: Play effects for level increase
        currentLevel++;
        UpdateLabels();
    }

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
    }
}
