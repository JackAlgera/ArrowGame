using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour {

    private Color initColor;
    private Color newColor;

    private float lerpValue;
    public float speed;

    // Arrow bop variables
    private Vector3 initPos;
    private float newPosY;
    private float currentPosY;
    public float bopValue;
    public float bopSpeed;
    private float lerpBopValue;
    private bool bopping = false;
    private bool goingUp = false;
    
    private void Awake()
    {
        // colors
        initColor = Random.ColorHSV(0f, 1, 0f, 1f, 0f, 1f, 0.25f, 0.25f);
        newColor = Random.ColorHSV(0f, 1, 0f, 1f, 0f, 1f, 0.25f, 0.25f);

        // Arrow bopping
        initPos = transform.position;
        bopping = false;
        goingUp = false;
    }

    void Update () {
        // Colors
		if(lerpValue <= 1f)
        {
            ChangeBarColors(initColor, newColor);
            lerpValue += Time.deltaTime / speed;
        }
        else
        {
            initColor = newColor;
            newColor = Random.ColorHSV(0f, 1, 0f, 1f, 0f, 1f, 0.25f, 0.25f);
            lerpValue = 0;
        }

        // Arrow bopping
        if(bopping)
        {
            if(goingUp)
            {
                if(lerpBopValue <= 1f)
                {
                    Vector3 temp = transform.position;
                    temp.y = Mathf.Lerp(currentPosY, newPosY, lerpBopValue);
                    transform.position = temp;
                    lerpBopValue += Time.deltaTime / bopSpeed;
                }
                else
                {
                    lerpBopValue = 0;
                    currentPosY = transform.position.y;
                    newPosY = initPos.y;
                    goingUp = false;
                }
            }
            else
            {
                if (lerpBopValue <= 1f)
                {
                    Vector3 temp = transform.position;
                    temp.y = Mathf.Lerp(currentPosY, newPosY, lerpBopValue);
                    transform.position = temp;
                    lerpBopValue += Time.deltaTime / bopSpeed;
                }
                else
                {
                    bopping = false;
                    transform.position = initPos;
                }
            }
        }
	}

    public void ChangeBarColors(Color initCol, Color newCol)
    {
        foreach (Transform bar in transform)
        {
            bar.GetComponent<SpriteRenderer>().color = Color.Lerp(initCol, newCol, lerpValue);
        }
    }

    public void BopBeatBars()
    {
        bopping = true;
        goingUp = true; 
        currentPosY = initPos.y;
        newPosY = initPos.y + Random.Range(bopValue / 2f, bopValue);
        lerpBopValue = 0f;
    }
}
