using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    public AudioClip musicClip1;
    public AudioSource musicSource;

    public Text highScoreText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerPrefs.SetInt("AdsPlayed", 0);

        musicSource.clip = musicClip1;
        musicSource.Play();

        InitialiseGame(false);
        UpdateText();
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
