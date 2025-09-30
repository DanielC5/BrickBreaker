using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //grabs the text for the current score
    public TMP_Text currentScoreText;
    //grabs the text foro the title
    public TMP_Text highScoreTitle;
    //grabs the list of highscore texts (top 3)
    public TMP_Text[] highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //show current score
        currentScoreText.text = "Score: " + GameManager.Instance.GetScore();

        //show top 3 high scores
        string[] topScores = GameManager.Instance.GetHighScores(PlayerPrefs.GetInt("Level"));

        for (int i = 0; i < highScoreText.Length && i < topScores.Length; i++)
        {
            highScoreText[i].text = topScores[i]; // fore each highscore text get the corresponding highscore
        }
        highScoreTitle.text = PlayerPrefs.GetInt("PlayerCount", 1) + " Player High Scores";
        //show the title thats respective to the player count so that P1 and P2 have different high score boards (obviously)


    }

}
