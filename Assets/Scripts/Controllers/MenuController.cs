using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
}
