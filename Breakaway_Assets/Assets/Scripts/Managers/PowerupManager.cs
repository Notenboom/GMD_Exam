using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameManager gamemanager;
    [SerializeField] private GameObject paddle;
    [SerializeField] private GameObject ball;
    #endregion

    #region EXTRA BALL POWERUP
    public void ExtraBall()
    {
        FindObjectOfType<AudioManager>().Play("powerup");
        gamemanager.UpdateBallCount(1);
    }
    #endregion

    #region LARGE PADDLE POWERUP
    public void LargePaddle()
    {
        StartCoroutine(CRLargePaddle());
    }

    IEnumerator CRLargePaddle()
    {
        FindObjectOfType<AudioManager>().Play("powerup");
        paddle.transform.localScale = new Vector2(2f, 1f);
        yield return new WaitForSecondsRealtime(10);
        paddle.transform.localScale = new Vector2(1f, 1f);
    }
    #endregion

    #region DOUBLE POINTS POWERUP
    public void DoublePoints()
    {
        StartCoroutine(CRDoublePoints());
    }

    IEnumerator CRDoublePoints()
    {
        FindObjectOfType<AudioManager>().Play("powerup");
        ball.GetComponent<Ball>().doublepoints = true;
        yield return new WaitForSecondsRealtime(10);
        ball.GetComponent<Ball>().doublepoints = false;
    }
    #endregion

    #region BALL SPEED UP POWERUP
    public void BallSpeed()
    {
        FindObjectOfType<AudioManager>().Play("powerup");
        ball.GetComponent<Ball>().UpdateBallSpeed(50f);
        ball.GetComponent<Ball>().score_speed_multiplier++;
    }
    #endregion
}
