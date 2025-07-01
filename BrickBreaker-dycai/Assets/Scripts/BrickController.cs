using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    //how many time to hit before the brick is destroyed
    public int numHits;
    //the number of points the brick gives
    public int pointsAwarded;
    //an object that we will spawn on hit
    public GameObject spawn;

    //speed up paddle powerup
    public bool speedUp;

    //make temporaryily get double points
    public bool doublePointsMult;

    //prevents double counting a hit
    private bool isHit;

    private ParticleSpawner ps;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isHit = false;

        ps = GameObject.Find("ParticleSpawner").GetComponent<ParticleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        //called from BallController
        if (!isHit)
        {
            if (numHits > 1)
            {
                //reduce the health by 1
                numHits -= 1;
                //play a flash animation and change the color
                StartCoroutine("HitAnimation");

            }
            else
            {
                isHit = true;
                ps.spawnParticles(this.transform,GetComponent<Renderer>());
                // subtract the brick form the total count
                GameManager.Instance.SubtractBrickCount();
                //add the score -- hint call the game manager function and send it the score value :0 so cool
                GameManager.Instance.AddScore(pointsAwarded);
                //play some animaition
                SoundManager.Instance.PlayBreak();
                StartCoroutine("FallAnimation");
                // if there is a spawn object, then create it
                if (spawn != null)
                {
                    //spawn the ball powerup
                    GameObject newBall = Instantiate(spawn, transform.position, transform.rotation);
                    GameManager.Instance.AddBallCount();
                    newBall.GetComponent<BallMovement>().moveBall();
                }
                if (speedUp)
                {
                    GameObject paddle = GameObject.Find("Paddle");
                    paddle.GetComponent<PaddleController>().SpeedUp(1.2f); //increase speed
                    GameObject paddle2 = GameObject.Find("Paddle2");
                    paddle2.GetComponent<PaddleController>().SpeedUp(1.2f); //increase speed
                }
                if (doublePointsMult) //i don;t think i will figure this out anytime soon
                {
                    GameManager.Instance.DoublePoints();
                }
                //Finally... destroy the brick
            }
        }


    }
    IEnumerator FallAnimation()
    {
        //dont let the brick hit the ball any more
        GetComponent<BoxCollider>().enabled = false;

        //make brick transparent
        Material mat = GetComponent<Renderer>().material;
        Color col = mat.color;
        mat.color = new Color(col.r, col.g, col.b, 0.5f);
        //move brick down
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(Vector3.down * 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);

        yield return null;
    }

    IEnumerator HitAnimation()
    {
        //make it flash
        Material mat = GetComponent<Renderer>().material;
        Color col = mat.color;
        Debug.Log(col.r);
        mat.color = new Color(1, 1, 1, 0.7f);
        yield return new WaitForSeconds(0.2f);
        mat.color = new Color(col.r + 0.1f, col.g + 0.1f, col.b + 0.1f, 1f);
    }
    

}
