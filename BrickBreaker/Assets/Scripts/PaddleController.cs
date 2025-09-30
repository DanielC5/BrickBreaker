using System.Collections;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PaddleController : MonoBehaviour
{
    public float speed; // 5 works nicely for speed

    private int direction; // 0 = not moving, -1 = left, 1 = right

    private float maxX; //sets the bounds of paddle movement
    private float minX;

    public int paddleNum; //paddle number (1 or 2)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = 0;
        maxX = 6.235f; // all numbers gotten  from just playing the game
        minX = -6.737f;
        if (PlayerPrefs.GetInt("PlayerCount", 1) == 1 && paddleNum == 2)
        {
            Destroy(this.gameObject); //destroys second paddle if playercount is 1
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PlayerCount", 1) == 2)
        {
            //sets keybinds for player 1 (wasd, well ad)
            if (paddleNum == 1)
            {
                // get user input
                if (Input.GetKey(KeyCode.A))
                {
                    direction = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }
            //sets keybinds for player 2 (left and right arrows)
            else
            {
                // get user input
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    direction = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }
        }
        else
        {   
            // get user input for single player
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("A Key was pressed, move left.");
                direction = -1;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("D Key was pressed, move right.");
                direction = 1;
            }
            else
            {
                direction = 0;
            }
        }

        // move the paddle
        Vector3 newPos = speed * Time.deltaTime * direction * Vector3.right + transform.position;

        //clamp this position
        if (newPos.x < maxX && newPos.x > minX)
        {
            transform.position = newPos;
        }
    }

    public void SpeedUp(float mult)
    {
        //speeds up with multiplier :D
        StartCoroutine("TempSpeed", mult);
    }

    IEnumerator TempSpeed(float multiplier)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(5); // increase speed by multipler for 5 seconds
        speed /= multiplier;
        yield return null;
    }

    public void MakeLonger(float mult)
    {

        //StartCoroutine("TempLonger", mult);
        //TODO: Implement this properly
        // never made the paddle longer
    }

    IEnumerator TempLonger(float multiplier)
    {
        //obsolete function
        gameObject.transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
        yield return null;
    }

    public void setPaddleNum(int num)
    {
        //sets the paddle number lol
        paddleNum = num;
    }
}
