using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{


    //attach to exit button, exits game
    public void ExitGame()
    {
        //plays the button noise
        SoundManager.Instance.PlayButton();
        Application.Quit();
    }

    public void EnterPlayerSelector()
    {
        //plays the button noise
        SoundManager.Instance.PlayButton();
        //changes the state to player select and then loads the scene
        GameManager.Instance.PlayerSelectState();
        SceneManager.LoadScene("PlayerSelectScene");
    }

    // move to level selector
    public void EnterLevelSelecter(int playerNum)
    {
        PlayerPrefs.SetInt("PlayerCount", playerNum); //save the number of players
        SoundManager.Instance.PlayButton(); //plays the button noise
        GameManager.Instance.LevelSelectState(); //changes state to level select
        SceneManager.LoadScene("LevelSelectScene"); //loads the elvel select scene and saves the player prefs
        PlayerPrefs.Save();
    }

    //funciton to open  scene 1
    public void PlayGame1()
    {
        SoundManager.Instance.PlayButton(); //plays the button noise
        SceneManager.LoadScene("GameScene3");  //todo fix the names - never got to do this ig (bugs out and doesnt work when the names flip)
        GameManager.Instance.ResetGameState(); //resets the game state (for like replay purposes)
        PlayerPrefs.SetInt("Level", 1); ///saves the level number for leaderboard purposes
        PlayerPrefs.Save();
    }

    //funciton to open game scene 2
    public void PlayGame2()
    {
        SoundManager.Instance.PlayButton(); //plays the button noise
        SceneManager.LoadScene("GameScene2"); // loads game scene 2
        GameManager.Instance.ResetGameState(); //resets the game state (for replay purposes)
        PlayerPrefs.SetInt("Level", 2); // saves the level number for leaderboard purposes
        PlayerPrefs.Save();
    }
    //funciton to open game scene 3
    public void PlayGame3()
    {
        SoundManager.Instance.PlayButton(); //plays the button noise
        SceneManager.LoadScene("GameScene1");  //todo fix the names
        GameManager.Instance.ResetGameState(); //resets the game state
        PlayerPrefs.SetInt("Level", 3); // saves the level number for leaderboard purposes
        PlayerPrefs.Save();
    }

}
