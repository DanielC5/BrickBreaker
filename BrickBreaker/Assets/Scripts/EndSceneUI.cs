using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TMP_Text currentScoreText;

    public TMP_Text highScoreTitle;
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
            highScoreText[i].text = topScores[i];
        }
        highScoreTitle.text = PlayerPrefs.GetInt("PlayerCount", 1) + " Player High Scores";


    }

}
