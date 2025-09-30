using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //the speed the ball -- make this private tmrw
    public float speed;

    //the direction the ball is moving
    private Vector3 dir;

    void Awake()
    {
        //make the ball as child to the paddle
        GameObject paddle = GameObject.Find("Paddle");
        transform.SetParent(paddle.transform);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //create random vector to start from
        dir = Vector3.Normalize(new Vector3(2*(Random.value-0.5f), Random.value, 0));
        //regenerate values to prevent directly up or horizontal cases
        while (dir.x == 0 || dir.x == 1 || dir.y == 0 || dir.y == 1)
        {
            dir = Vector3.Normalize(new Vector3(2*(Random.value-0.5f), Random.value, 0));
        }

    }

    // Update is called once per frame
    void Update()
    {
        //starts the ball moving when up arrow or W key pressed
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && GameManager.Instance.state == GameManager.GameState.Game)
        {
            //sets the state to the playing state
            GameManager.Instance.state = GameManager.GameState.Playing;
            //turn 18 and release the parent
            transform.SetParent(null);
        }

        if (GameManager.Instance.state == GameManager.GameState.Playing)
        {
            // move the ball if currently playing the game
            transform.Translate(dir * speed * Time.deltaTime);

        }
    }

    //detects collisions
    private void OnCollisionEnter(Collision collision)
    {
        //bounce the ball
        Vector3 normal = collision.GetContact(0).normal;
        Vector3 reflected = Vector3.Reflect(dir, normal);
        reflected.z = 0;
        dir = Vector3.Normalize(reflected);
        if (dir == Vector3.right || dir == Vector3.left || dir == Vector3.down || dir == Vector3.up)
        {
            dir.y += 0.1f;
        }

        //hit a brick?
        if (collision.gameObject.CompareTag("Brick"))
        {
            //tells the brick it got hit (because its a brick and needs to be told that)
            collision.gameObject.GetComponent<BrickController>().Hit();

            //increase the speed wen hit brick;
            ChangeSpeed(0.2f);
            //play silly brick bounce sfx
            SoundManager.Instance.PlayBrickBounce();
        }

        //destroy ball when hitting the bottom wall
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.DestroyBall(this.gameObject);
            SoundManager.Instance.PlayDeath();
        }

        //decrease the speed when hitting the paddle
        if (collision.gameObject.CompareTag("Paddle"))
        {
            ChangeSpeed(-0.2f);
            SoundManager.Instance.PlayBounce();
        }


        //increase the speed when hitting the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeSpeed(0.1f);
            SoundManager.Instance.PlayBounce();

        }


    }

    //function to increase the ball speed and decrease the ballspeed
    private void ChangeSpeed(float change)
    {
        speed += change;
    }

    public void moveBall()
    {
        //releases the parent 
        transform.SetParent(null);
        dir = Vector3.down;

    }
}
