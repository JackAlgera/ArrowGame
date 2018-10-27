using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreShaker : MonoBehaviour {

    private Vector3 initPosition;
    private Vector3 originPosition;
    public float init_shake_decay;
    public float init_shake_intensity;
    private float shake_decay;
    private float shake_intensity;

    private void Awake()
    {
        initPosition = gameObject.GetComponent<RectTransform>().position;
    }

    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            shake_intensity -= shake_decay;
            if(shake_intensity <= 0)
            {
                transform.position = initPosition;
            }
        }
    }

    public void Shake()
    {
        originPosition = transform.position;
        shake_intensity = init_shake_decay;
        shake_decay = init_shake_intensity;
    }
}