using UnityEngine;
using TMPro;
public class WinLoseUI : MonoBehaviour
{
    //begining: no panel no text
    //end: yes panel yes text
    //1. win and YAY win text
    //2. lose and 3: lose text

    public GameObject panel;
    public GameObject endText;
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel.SetActive(false);
        text = endText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.Win)
        {
            panel.SetActive(true);
            text.SetText("You win! Congrats!");
        }
        else if (GameManager.Instance.state == GameManager.GameState.Lose)
        {
            panel.SetActive(true);
            text.SetText("You lose :c Try again 3:");
        }
    }
}
