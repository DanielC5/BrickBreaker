using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickSpawner : MonoBehaviour
{
    //all the different colors and kinds of bricks as prefabs
    public GameObject[] brickPrefabs;
    //number of rows (preset)
    private int numRows = 5;
    //number of columns preset
    private int numCols = 13;
    //width of an actual brick (preset)
    private float brickWidth = 1.21f;
    //height of an actual brick (preset)
    private float brickHeight = 0.65f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get scene name to determine which spawning pattern to use
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals("GameScene1"))
        {
            SpawnBricks(1); // if the scene is "main" then use pattern 1
        }
        else if (sceneName.Equals("GameScene2"))
        {
            SpawnBricks(2); // if the scene is "main" then use pattern 2
        }
        else
        {
            SpawnBricks(3); // if nothing else then just use pattern 3
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBricks(int pattern)
    {
        //for loop to spawn the bricks in a grid like fashion
        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < numCols; c++)
            {
                //sets the index by calling the funciton Pattern Index with the row, column, and pattern number
                int brickIndex = PatternIndex(r, c, pattern);

                if (brickIndex != -1) // -1 means no brick
                {
                    //creates a new vector for the brick position
                    Vector3 brickPos = new Vector3(transform.position.x + c * brickWidth, transform.position.y - brickHeight * r, transform.position.z);
                    //creates a new brick at the position=
                    Instantiate(brickPrefabs[brickIndex], brickPos, transform.rotation);
                    //adds to number of bricks that need to be destrouyed when you play the game
                    GameManager.Instance.AddBrickCount();
                }
                
            }
        }

    }

    public int PatternIndex(int row, int col, int patternNum) {
        int brickIndex = -1; // -1 means no brick
        if (patternNum == 1)
        {
            //Index Selection
            //Pattern: Reflecting Rainbow
            //Mods the sum of the row and column by 12 so the pattern appears diagonally
            brickIndex = (row + col) % 12;
            //If the Index is greater or equal to 7, subtract it from 12 to get it to rebound back to index 0 when it hits index 6
            if (brickIndex >= 7)
            {
                brickIndex = 12 - brickIndex;
            }
        }
        else if (patternNum == 2)
        {
            //Index Selection
            //Pattern: Eyes 
            // just makes eyes lol idk
            brickIndex = 0; //sets the entire area to red
            if (col >= 5 && col <= 7)
            {
                brickIndex = -1; //removes bricks from columns 5-7
            }
            if (row > 0 && row < 4 && ((col > 0 && col < 4) || (col > 8 && col < 12)))
            {
                brickIndex = 4; //colors a 3 by 3 square of blue on both ends ofset from the border
            }
            if (row > 1 && row < 3 && ((col > 1 && col < 3) || (col > 9 && col < 11)))
            {
                brickIndex = 3; // colors a 1 by 1 square of green in the middle of the squares
            }


        }
        else if (patternNum == 3)
        {
            //Index Selection
            //Pattern: t
            //what can i say it looks like a t
            if (row == 2 || col == 5) //draws stripes one by one and overrides the colors as it goes on to create the effect
            {
                brickIndex = 2;
            }
            if (row == 1 || col == 4)
            {
                brickIndex = 3;
            }
            
            
        }
        return brickIndex;

    }
}
