using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] public float ball_startforce = 300f;
    private Transform ball_holder_position;
    private Rigidbody2D ball_rigidbody;
    private bool ball_hold;
    [SerializeField] private Transform break_particle;
    [SerializeField] private Transform[] powerups;
    [SerializeField] private int powerdroppercentage = 10;
    [SerializeField] private GameManager gamemanager;
    public bool doublepoints = false;
    public int score_speed_multiplier;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ball_holder_position = GameObject.Find("Ball_Holder").GetComponent<Transform>();
        ball_rigidbody = GetComponent<Rigidbody2D>();
        score_speed_multiplier = 1;

    }

    // Update is called once per frame
    void Update()
    {
        #region GAME OVER
        if (gamemanager.gameover == true)
        {
            return;
        }
        #endregion

        if (ball_hold == false)
        {
            transform.position = ball_holder_position.position;
        }

        #region LAUNCH BALL
        if (Input.GetButtonDown("Jump") && ball_hold == false)
        {
            gamemanager.amountofbricks = GameObject.FindGameObjectsWithTag("brick").Length;
            ball_hold = true;
            ball_rigidbody.AddForce(Vector2.up * ball_startforce);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region GAME OVER
        if (gamemanager.gameover == true)
        {
            ResetBall();
        }
        #endregion
    }

    #region BALL EXIT SCREEN LOGIC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "edge_trigger")
        {
            FindObjectOfType<AudioManager>().Play("death");
            ball_rigidbody.velocity = Vector2.zero;
            ball_hold = false;

            //gamemanager function can be replaced with event
            gamemanager.UpdateBallCount(-1);
        }
        if (gamemanager.amountofbricks <= 0)
        {
            FindObjectOfType<AudioManager>().Play("death");
            gamemanager.LevelDone();
        }
    }
    #endregion

    #region BALL COLLISION LOGIC
    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region SCREEN EDGE COLLISION
        if (collision.transform.tag == "screen_edge")
        {
            FindObjectOfType<AudioManager>().Play("brick_dammaged");
        }
        #endregion

        #region BRICK COLLISION
        if (collision.transform.tag == "brick")
            {
            Brick brickscript = collision.gameObject.GetComponent<Brick>();
            if (brickscript.health > 1)
            {
                brickscript.DammageBrick();
            }
            else
            {
                if(doublepoints == true)
                {
                    gamemanager.UpdateScoreCount(brickscript.score * score_speed_multiplier * 2);
                }
                if (doublepoints == false)
                {
                    gamemanager.UpdateScoreCount(brickscript.score * score_speed_multiplier);
                }
                //gamemanager function can be replaced with event
                gamemanager.UpdateBrickAmount();

                FindObjectOfType<AudioManager>().Play("brick_destroyed");
                Transform particle_break = Instantiate(break_particle, collision.transform.position, collision.transform.rotation);
                Destroy(collision.gameObject);
                Destroy(particle_break.gameObject, 2f);


                int randomdrop = Random.Range(1, 101);
                if (randomdrop < powerdroppercentage)
                {
                    int randompower = Random.Range(0, powerups.Length);
                    Instantiate(powerups[randompower], collision.transform.position, collision.transform.rotation);
                }
            }
        }
        #endregion
    }
    #endregion

    public void ResetBall()
    {
        ball_rigidbody.velocity = Vector2.zero;
        ball_hold = false;
    }

    #region BALL SPEED CHANGE
    public void UpdateBallSpeed(float change)
    {
        ball_rigidbody.AddForce(ball_rigidbody.velocity.normalized * Time.deltaTime * change);
        ball_startforce += change;
    }
    #endregion
}
