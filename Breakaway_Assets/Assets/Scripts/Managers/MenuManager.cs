using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region MENU BUTTON CLICK LOGIC
    public void Tutorial()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Play()
    {
        SceneManager.LoadScene("Game_One");
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region TEST SOUND EFFECT BUTTON
    public void TestEffects()
    {
        Debug.Log(PlayerPrefs.GetFloat("Masterpref"));
        Debug.Log(PlayerPrefs.GetFloat("Backgroundpref"));
        Debug.Log(PlayerPrefs.GetFloat("Effectspref"));

        int randompower = Random.Range(1, 6);
        if (randompower == 1)
        {
            FindObjectOfType<AudioManager>().Play("brick_destroyed");
        }
        if (randompower == 2)
        {
            FindObjectOfType<AudioManager>().Play("brick_dammaged");
        }
        if (randompower == 3)
        {
            FindObjectOfType<AudioManager>().Play("powerup");
        }
        if (randompower == 4)
        {
            FindObjectOfType<AudioManager>().Play("death");
        }
        if (randompower == 5)
        {
            FindObjectOfType<AudioManager>().Play("suction");
        }
    }
    #endregion
}
