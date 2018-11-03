using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour {

    private Color initColor;
    private Color newColor;

    private float lerpValue;
    public float speed;

    private void Awake()
    {
        lerpValue = 0f;
        initColor = Random.ColorHSV(0f, 1, 0.5f, 1f, 0f, 1f, 1f, 1f);
        newColor = Random.ColorHSV(0f, 1, 0.5f, 1f, 0f, 1f, 1f, 1f);
    }

    void Update()
    {
        if (lerpValue <= 1f)
        {
            gameObject.GetComponent<Text>().color = Color.Lerp(initColor, newColor, lerpValue);
            lerpValue += Time.deltaTime / speed;
        }
        else
        {
            initColor = newColor;
            newColor = Random.ColorHSV(0f, 1, 0.5f, 1f, 0f, 1f, 1f, 1f);
            lerpValue = 0f;
        }
    }
}
