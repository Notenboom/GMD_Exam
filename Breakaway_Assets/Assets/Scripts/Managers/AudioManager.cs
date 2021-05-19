using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;


/*
 * BUG PROBLEM:
 * Still have an issue with saving the slider and sound state across diffrent launched from the application
 */

public class AudioManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] public Sounds[] sounds;
    [SerializeField] public Slider mastersli, backgroundsli, effectssli;
    [SerializeField] private float mastervol, backgroundvol, effectsvol;
    public static AudioManager instance;
    private static readonly string Firstplay = "Firstplay";
    private static readonly string Masterpref = "Masterpref";
    private static readonly string Backgroundpref = "Backgroundpref";
    private static readonly string Effectspref = "Effectspref";
    private int firstplay;
    public bool grabsliders;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        grabsliders = false;

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

        #region CREATE AUDIO SOURCES
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playonAwake;

            if (s.background == true)
            {
                s.source.volume = mastervol * backgroundvol;
            }
            if (s.effects == true)
            {
                s.source.volume = mastervol * effectsvol;
            }
        }
        #endregion

        #region FIRST TIME LAUNCH CHECK AND SLIDER POPULATION
        firstplay = PlayerPrefs.GetInt(Firstplay);
        if (firstplay == 0)
        {
            mastervol = 0.5f;
            backgroundvol = 0.1f;
            effectsvol = 1f;
            mastersli.value = mastervol;
            backgroundsli.value = backgroundvol;
            effectssli.value = effectsvol;
            PlayerPrefs.SetInt(Firstplay, 1);

            Debug.Log("firsttime " + PlayerPrefs.GetFloat(Backgroundpref));
            Debug.Log("firsttime " + backgroundvol);
            Debug.Log("firsttime " + backgroundsli.value);
        }
        else
        {
            mastervol = PlayerPrefs.GetFloat(Masterpref);
            backgroundvol = PlayerPrefs.GetFloat(Backgroundpref);
            effectsvol = PlayerPrefs.GetFloat(Effectspref);
            mastersli.value = mastervol;
            backgroundsli.value = backgroundvol;
            effectssli.value = effectsvol;

            Debug.Log("start "+PlayerPrefs.GetFloat(Backgroundpref));
            Debug.Log("start " + backgroundvol);
            Debug.Log("start " + backgroundsli.value);
            PlayerPrefs.SetInt(Firstplay, 0);
        }
        #endregion

        Play("background");
    }
    private void FixedUpdate()
    {
        #region GRAB SLIDERS WHEN NAVIGATING BACK TO MAIN MENU SCENE
        if (grabsliders == true)
        {
            mastersli = GameObject.Find("Slider_MasterV").GetComponent<Slider>();
            backgroundsli = GameObject.Find("Slider_BackgroundV").GetComponent<Slider>();
            effectssli = GameObject.Find("Slider_EffectsV").GetComponent<Slider>();
            
            mastersli.value = mastervol;
            backgroundsli.value = backgroundvol;
            effectssli.value = effectsvol;

            UpdateSound();

            grabsliders = false;
        }
        #endregion

        mastersli.onValueChanged.AddListener(delegate { UpdateSound(); });
        backgroundsli.onValueChanged.AddListener(delegate { UpdateSound(); });
        effectssli.onValueChanged.AddListener(delegate { UpdateSound(); });

    }

    #region PLAY LOGIC TO BE CALLED FROM WHERE NEEDED
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " Audio clip was not found");
            return;
        }
        s.source.Play();
    }
    #endregion

    #region STOP PLAY LOGIC TO BE CALLED FROM WHERE NEEDED
    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " Audio clip was not found");
            return;
        }
        s.source.Stop();
    }
    #endregion

    #region UPDATE THE SOUND VOLUME WHEN SLIDERS ARE MOVED
    public void UpdateSound()
    {
        mastervol = mastersli.value;
        backgroundvol = backgroundsli.value;
        effectsvol = effectssli.value;

        PlayerPrefs.SetFloat(Masterpref, mastervol);
        PlayerPrefs.SetFloat(Backgroundpref, backgroundvol);
        PlayerPrefs.SetFloat(Effectspref, effectsvol);
        foreach (Sounds s in sounds)
        {
            
            
            if (s.background == true)
            {
                s.source.volume = mastersli.value * backgroundsli.value;     
            }
            if (s.effects == true)
            {
                s.source.volume = mastersli.value * effectssli.value;
            }
        }
    }
    #endregion
}
