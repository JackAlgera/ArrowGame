using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundImageController : MonoBehaviour {

    public Sprite buttonOn;
    public Sprite buttonOff;

    private Toggle tog;

    private void Awake()
    {
        tog = gameObject.GetComponent<Toggle>();

        Debug.Log("" + PlayerPrefs.GetInt("PlayMusic"));

        if (PlayerPrefs.GetInt("PlayMusic") == 1)
        {
            tog.isOn = true;
        }
        else
        {
            tog.isOn = false;
        }

        if (tog.isOn)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = buttonOn; 
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = buttonOff;
        }
    }

    public void ChangeSprite()
    {
        if (tog.isOn)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = buttonOn;
            PlayerPrefs.SetInt("PlayMusic", 1);
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = buttonOff;
            PlayerPrefs.SetInt("PlayMusic", 0);
        }
    }
}
