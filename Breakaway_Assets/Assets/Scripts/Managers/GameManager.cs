using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private int score;
    [SerializeField] private int balls;
    [SerializeField] private Text intscore_TXT;
    [SerializeField] private Text intballs_TXT;
    [SerializeField] private GameObject gameover_panel;
    public bool gameover;
    [SerializeField] public int amountofbricks;
    [SerializeField] private LeaderboardManager leaderboardscript;
    [SerializeField] private GameObject[] level;
    public delegate void GameOverEvent();
    public static event GameOverEvent gameoverevent;
    [SerializeField] private GameObject ball;
    private GameObject playablelevel;
    private int randomlevel;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        intscore_TXT.text = "" + score;
        intballs_TXT.text = "" + balls;

        randomlevel = Random.Range(0, level.Length);
        NewLevel();
    }

    #region UPDATE THE BALL COUNT
    public void UpdateBallCount(int ballchange)
    {
            balls += ballchange;
            if (balls <= 0)
            {
                balls = 0;
                GameOver();
            }
            intballs_TXT.text = "" + balls;
    }
    #endregion

    #region UPDATE THE SCORE COUNT
    public void UpdateScoreCount(int scorechange)
    {
        score += scorechange;
        intscore_TXT.text = "" + score;

            if (leaderboardscript.gethighscore() < score)
            {
                leaderboardscript.sethighscore(score);
                leaderboardscript.updatehighscoredisplay();
            }
    }
    #endregion

    #region GAME OVER
    private void GameOver()
    {
        if(gameoverevent != null)
        {
            gameoverevent();
        }
        gameover_panel.SetActive(true);
        gameover = true;
    }
    #endregion

    #region GAME OVER BUTTONS
    public void BTN_Click_PlayAgain()
    {
        SceneManager.LoadScene("Game_One");
    }
    public void BTN_Click_MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().grabsliders = true;
    }
    #endregion

    #region UPDATE BRICK AMMOUNT
    public void UpdateBrickAmount()
    {
        amountofbricks--;
    }
    #endregion

    #region GENERATE A NEW LEVEL
    public void NewLevel()
    {
        Debug.Log("new level");
        if (playablelevel != null)
        {
            Destroy(playablelevel);
        }
            randomlevel = Random.Range(0, level.Length);
            playablelevel = Instantiate(level[randomlevel], new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void LevelDone()
    {
        Debug.Log("level done");
        if (amountofbricks <= 0)
        {
            ball.GetComponent<Ball>().ResetBall();
            NewLevel();
        }
        else
        {

        }
    }
    #endregion
}
