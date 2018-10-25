using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    public Text highScoreText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

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
}
