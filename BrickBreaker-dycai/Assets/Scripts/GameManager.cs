using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

    public GameState state;

    public GameObject paddle;

    public int totalScore;
    public int ballCount;
    public int brickCount;
    private TMP_Text scoreText;

    private int scoreMultiplier;

    private void Awake()
    {
        //makes sure that there's only one game manager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = GameState.StartMenu;
        scoreMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Playing)
        {
            CheckGameEnd();
        }
    }

    public void ResetGameState()
    {
        //when I play again
        state = GameState.Game;
        totalScore = 0;
        ballCount = 1;
        brickCount = 0;
        SoundManager.Instance.StopMenuMusic();
        SoundManager.Instance.PlayMusic();

    }

    public void PlayerSelectState()
    {
        state = GameState.PlayerMenu;
    }



    public void LevelSelectState()
    {
        state = GameState.LevelMenu;
    }

    public void CheckGameEnd()
    {
        // to end the game, 1. you clear all blocks, 2. the ball is destroyed
        if (ballCount <= 0 || brickCount <= 0)
        {
            if (ballCount <= 0)
            {
                state = GameState.Lose;
            }
            else
            {
                state = GameState.Win;
            }
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
        Debug.Log("loaded scene");

    }
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
        //TODO: update score text
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (scoreMultiplier == 1)
        {
            GameObject scoreObj = GameObject.Find("Score");
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            scoreText.text = "Score: " + totalScore;
        }
        else
        {
            GameObject scoreObj = GameObject.Find("Score");
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            scoreText.text = "Score: " + totalScore + "   x2";
        }


    }

    public string[] GetHighScores(int level)
    {
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
        scoreMultiplier = 2;
        GameObject scoreObj = GameObject.Find("Score");
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + totalScore + "   x2";
        yield return new WaitForSeconds(30);
        scoreMultiplier = 1;
        UpdateScoreText();
        yield return null;
    }

    

    
}
