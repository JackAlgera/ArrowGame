using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    public AudioClip[] musicClip;
    public AudioSource musicSource;
    private int currentMusicId;

    public Text highScoreText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerPrefs.SetInt("AdsPlayed", 0);

        currentMusicId = Random.Range(0, musicClip.Length);

        musicSource.clip = musicClip[currentMusicId];
        musicSource.Play();

        InitialiseGame(false);
        UpdateText();
    }

    private void FixedUpdate()
    {
        if (!musicSource.isPlaying)
        {
            int newMusicId = Random.Range(0, musicClip.Length);
            while (currentMusicId == newMusicId)
            {
                newMusicId = Random.Range(0, musicClip.Length);
            }
            currentMusicId = newMusicId;
            musicSource.clip = musicClip[currentMusicId];
            musicSource.Play();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void InitialiseGame(bool reset)
    {
        if(reset)
        {
            PlayerPrefs.DeleteAll();
        }

        if(!PlayerPrefs.HasKey("FirstLaunch"))
        {
            PlayerPrefs.SetInt("FirstLaunch", 1);
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("PlayMusic", 1);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    public void UpdateText()
    {
        highScoreText.text = "" + GetHighScore();
    }

    public void StopSound()
    {
        musicSource.mute = !musicSource.mute;
    }
}
