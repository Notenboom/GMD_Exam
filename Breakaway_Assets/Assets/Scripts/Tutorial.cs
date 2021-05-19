using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
            GameObject.Find("AudioManager").GetComponent<AudioManager>().grabsliders = true;
        }
    }
}
