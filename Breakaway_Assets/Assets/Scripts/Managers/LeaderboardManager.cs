using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    #region VARIABLES
    public static LeaderboardManager instance;
    private int highscore;
    [SerializeField] private Text highscoredisplay;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region GET AND DISPLAY HIGHSCORE
        highscore = PlayerPrefs.GetInt("HIGHSCORE");
        highscoredisplay.text = "" + highscore;
        #endregion

        #region DO NOT DESTROY ON LOAD
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    #region GET SET UPDATE HIGHSCORE FUNCTIONS
    public int gethighscore()
    {
        return highscore;
    }

    public void sethighscore(int score)
    {
        highscore = score;
        PlayerPrefs.SetInt("HIGHSCORE", highscore);
    }

    public void updatehighscoredisplay()
    {
        highscoredisplay.text = "" + highscore;
    }
    #endregion
}
