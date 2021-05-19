using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameManager gamemanager;
    [SerializeField] private Ball ballscript;
    [SerializeField] private Animator animator;

    [SerializeField] private float movement_speed = 10f;
    [SerializeField] private float edge_right;
    [SerializeField] private float edge_left;

    private float horizontal_move;

    #region ENGINE SOUND //Deemed to annoying to keep enabled
    private bool engine;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ballscript = GameObject.Find("Ball").GetComponent<Ball>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region GAME OVER
        if (gamemanager.gameover == true)
        {
            gameObject.SetActive(false);
            return;
        }
        #endregion

        #region MOVEMENT LOGIC
        horizontal_move = Input.GetAxis("Horizontal");
        animator.SetFloat("direction", horizontal_move);
        transform.Translate(Vector2.right * horizontal_move * Time.deltaTime * movement_speed);
        if(transform.position.x < edge_left)
        {
            transform.position = new Vector2(edge_left, transform.position.y);
        }
        if (transform.position.x > edge_right)
        {
            transform.position = new Vector2(edge_right, transform.position.y);
        }
        #endregion

        #region ENGINE SOUND //Deemed to annoying to keep enabled
        /*
        if(horizontal_move == 0 && engine == true)
        {
            FindObjectOfType<AudioManager>().Stop("engine_rumble");
            engine = false;
        }
        */
        #endregion
    }
    private void FixedUpdate()
    {
        #region ENGINE SOUND //Deemed to annoying to keep enabled
        /*
        if (horizontal_move != 0 && engine == false)
        {
            FindObjectOfType<AudioManager>().Play("engine_rumble");
            engine = true;
        }
        */
        #endregion
    }

    #region PADDLE AND BALL COLLISION LOGIC
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ball")
        {
            FindObjectOfType<AudioManager>().Play("brick_dammaged");
        }

        if (collision.transform.tag == "ball" && gamemanager.amountofbricks <= 0){
            FindObjectOfType<AudioManager>().Play("suction");
            gamemanager.LevelDone();
        }
    }
    #endregion
}
