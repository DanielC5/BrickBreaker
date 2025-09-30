using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //gets the only instance of gamemanager
    public static GameManager Instance;
    //finite state machine for this game
    public enum GameState
    {
        StartMenu,
        PlayerMenu,
        LevelMenu,
        Game,
        Playing,
        Win,
        Lose,
        End
    }

    //the gamestate of the entire games
    public GameState state;
    //gets the paddle of the game
    public GameObject paddle;
    //keeps track of the total score of the user
    public int totalScore;

    //keeps track of the ball count (for when you get the brick that gives you an extra ball)
    public int ballCount;
    //keeps track of brick count to see when you win (when num bricks = 0)
    public int brickCount;

    //grabs the scoretext to update when score increases
    private TMP_Text scoreText;

    //keeps track of the current multiplier to display and use for calculations

    private int scoreMultiplier;

    private void Awake()
    {
        //makes sure that there's only one game manager
        if (Instance == null)
        {
            //keeps the game manager
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //destroys any other game manager
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //essentially resets everything and then changes the state
        state = GameState.StartMenu;
        scoreMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //only checks if the game ends when you are actually playing the game
        if (state == GameState.Playing)
        {
            CheckGameEnd();
        }
    }

    public void ResetGameState()
    {
        //when I play again resets all my stats and changes the music to mathc
        state = GameState.Game;
        totalScore = 0;
        ballCount = 1;
        brickCount = 0;
        SoundManager.Instance.StopMenuMusic();
        SoundManager.Instance.PlayMusic();

    }

    public void PlayerSelectState()
    {
        //sets state to number of players menu state
        state = GameState.PlayerMenu;
    }



    public void LevelSelectState()
    {
        //sets state to level menu state
        state = GameState.LevelMenu;
    }

    public void CheckGameEnd()
    {
        // to end the game, 1. you clear all blocks, 2. the ball is destroyed
        if (ballCount <= 0 || brickCount <= 0)
        {
            if (ballCount <= 0)
            {
                //lose if you run out of balls
                state = GameState.Lose;
            }
            else
            {
                //the other is if you win and brick count is 0 (or less somehow) and the game state is set to win
                state = GameState.Win;
            }
            //end game state that allows for the " you win or you lose" text
            StartCoroutine("EndGame");
            SaveHighScore(PlayerPrefs.GetInt("Level"));
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlayMenuMusic();
        }
    }

    IEnumerator EndGame()
    {

        yield return new WaitForSeconds(3);
        state = GameState.End;
        SceneManager.LoadScene("EndScene");
        //Debug.Log("loaded scene");

    }
    //following are kinda self explanatory
    public void AddBallCount()
    {
        ballCount += 1;
    }
    public void SubtractBallCount()
    {
        ballCount -= 1;
    }
    public void AddBrickCount()
    {
        brickCount += 1;
    }
    public void SubtractBrickCount()
    {
        brickCount -= 1;
    }

    public void DestroyBall(GameObject ball)
    {
        Destroy(ball);
        SubtractBallCount();
    }

    public int GetScore()
    {
        return totalScore;
    }

    public void AddScore(int points)
    {
        totalScore += points * scoreMultiplier;
        //TODO: update score text - done
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        // updates score text
        if (scoreMultiplier == 1)
        {
            GameObject scoreObj = GameObject.Find("Score");
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            scoreText.text = "Score: " + totalScore;
        }
        else //adds the multiplier text to the end of the score while the multiplier is not 1
        {
            GameObject scoreObj = GameObject.Find("Score");
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            scoreText.text = "Score: " + totalScore + "   x2";
        }


    }

    public string[] GetHighScores(int level)
    {
        //array of 3 strings for top 3 scores
        string[] results = new string[3];

        for (int i = 0; i < 3; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + level + "P" + PlayerPrefs.GetInt("PlayerCount", 1) + i.ToString(), 0);
            results[i] = $"{i + 1}. {score}";
        }
        return results;

    }

    public void SaveHighScore(int level)
    {
        int newScore = totalScore;

        //load existing scores
        int[] scores = new int[4];

        for (int i = 0; i < 3; i++)
        {
            scores[i] = PlayerPrefs.GetInt("HighScore" + level + "P" + PlayerPrefs.GetInt("PlayerCount", 1) + i.ToString(), 0);
        }

        //add new score to the array and sort it
        scores[3] = newScore;
        //sort scores from smallest to biggest
        System.Array.Sort(scores);

        //reverse it so highest score comes first
        System.Array.Reverse(scores);

        //save top 3 sorted scores
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("HighScore" + level + "P" + PlayerPrefs.GetInt("PlayerCount", 1) + i.ToString(), scores[i]);
        }
        PlayerPrefs.Save();

    }

    public void DoublePoints()
    {
        StartCoroutine("DoublePoint");
    }

    IEnumerator DoublePoint()
    {
        //temporarily gives the player double point multiplier 
        scoreMultiplier = 2;
        GameObject scoreObj = GameObject.Find("Score"); //updates the score text as well (since it's possible score doesn't change when you get double point)
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + totalScore + "   x2";
        yield return new WaitForSeconds(30); //ends after 30 seconds 
        scoreMultiplier = 1;
        UpdateScoreText(); //updates the score text again
        yield return null;
    }

    

    
}
