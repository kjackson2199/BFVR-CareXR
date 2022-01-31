using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrossSceneMusic : MonoBehaviour
{
    #region Singleton
    public static CrossSceneMusic Instance { get { return _instance; } }
    static CrossSceneMusic _instance;
    #endregion

    int musicID = 1;
    bool isPlaying = false;
    public AudioSource source;
    public AudioClip c1;
    public AudioClip c2;
    public AudioClip c3;
    public Text t;
    public GameObject creditPanel;

    GameObject musicButton;

    void Awake()
    {
        if (!_instance)
            _instance = this;
        if (_instance != this && _instance)
            DestroyImmediate(this.gameObject);

        DontDestroyOnLoad(this); //music will persist between scenes.
    }

    private void Update()
    {
        // Check for music button
        musicButton = GameObject.FindGameObjectWithTag("MusicButton");
        t = musicButton.GetComponentInChildren<Text>();
        UpdateMusicText();
    }

    void UpdateMusicText()
    {
        switch(musicID)
        {
            case 0:
                t.text = "Play Music";
                break;
            case 1:
                t.text = "Change Music";
                break;
            case 2:
                t.text = "Change Music";
                break;
            case 3:
                t.text = "Stop Music";
                break;
        }
    }

    void Play()
    {
        if (!isPlaying)
        {
            creditPanel.SetActive(true);
            isPlaying = true;
            musicID = 1;
            source.clip = c1;
            source.Play();
            UpdateMusicText();
        }
        else if (isPlaying && musicID == 1)
        {
            musicID = 2;
            source.clip = c2;
            source.Play();
            UpdateMusicText();
        }
        else if (isPlaying && musicID == 2)
        {
            musicID = 3;
            source.clip = c3;
            source.Play();
            UpdateMusicText();
        }
        else if (isPlaying && musicID == 3)
        {
            StopMusic();
        }
    }

    public void PlayMusic()
    {
        Instance.Play();
    }

    public void StopMusic()
    {
        Instance.source.Stop();
        isPlaying = false;
        t.text = "Play Music";
    }
}
