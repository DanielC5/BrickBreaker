using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{


    //attach to exit button, exits game
    public void ExitGame()
    {
        SoundManager.Instance.PlayButton();
        Application.Quit();
    }

    public void EnterPlayerSelector()
    {
        SoundManager.Instance.PlayButton();
        GameManager.Instance.PlayerSelectState();
        SceneManager.LoadScene("PlayerSelectScene");
    }

    // move to level selector
    public void EnterLevelSelecter(int playerNum)
    {
        PlayerPrefs.SetInt("PlayerCount", playerNum); //save the number of players
        SoundManager.Instance.PlayButton();
        GameManager.Instance.LevelSelectState();
        SceneManager.LoadScene("LevelSelectScene");
        PlayerPrefs.Save();
    }

    //funciton to open  scene 1
    public void PlayGame1()
    {
        SoundManager.Instance.PlayButton();
        SceneManager.LoadScene("GameScene3");  //todo fix the names
        GameManager.Instance.ResetGameState();
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.Save();
    }

    //funciton to open game scene 2
    public void PlayGame2()
    {
        SoundManager.Instance.PlayButton();
        SceneManager.LoadScene("GameScene2");
        GameManager.Instance.ResetGameState();
        PlayerPrefs.SetInt("Level", 2);
        PlayerPrefs.Save();
    }
    //funciton to open game scene 3
    public void PlayGame3()
    {
        SoundManager.Instance.PlayButton();
        SceneManager.LoadScene("GameScene1");  //todo fix the names
        GameManager.Instance.ResetGameState();
        PlayerPrefs.SetInt("Level", 3);
        PlayerPrefs.Save();
    }

}
