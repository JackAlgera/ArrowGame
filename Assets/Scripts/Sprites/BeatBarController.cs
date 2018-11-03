using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBarController : MonoBehaviour {

    // Size controller
    public float initScaleY;
    public float initScaleRange;

    public float smallScaleY;
    public float smallScaleRange;

    public float newScaleY;
    public float oldScaleY;

    public int nbrProcsInNewScaleY;

    public float speed;
    private float lerpValue;

    // Color controller
    

	void Awake () {
        initScaleY = transform.localScale.y;
        oldScaleY = initScaleY;

        smallScaleY = initScaleY + Random.Range(-initScaleRange, initScaleRange);
        newScaleY = smallScaleY + Random.Range(-smallScaleRange, smallScaleRange);

        lerpValue = 0f;
        nbrProcsInNewScaleY = Random.Range(2, 6);
    }
	
	void FixedUpdate () {
        if(nbrProcsInNewScaleY > 0)
        {
            if (lerpValue <= 1)
            {
                Vector3 temp = transform.localScale;
                temp.y = Mathf.Lerp(oldScaleY, newScaleY, lerpValue);
                transform.localScale = temp;

                lerpValue += Time.deltaTime * speed;
            }
            else
            {
                nbrProcsInNewScaleY--;
                lerpValue = 0;
                oldScaleY = newScaleY;
                newScaleY = smallScaleY + Random.Range(-smallScaleRange, smallScaleRange);
            }
        }
        else
        {
            nbrProcsInNewScaleY = Random.Range(2, 6);
            lerpValue = 0;

            smallScaleY = initScaleY + Random.Range(-initScaleRange, initScaleRange);
            newScaleY = smallScaleY + Random.Range(-smallScaleRange, smallScaleRange);
        }
	}
}
