using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitBackground : MonoBehaviour {

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = transform.localScale;

        scale.y *= cameraSize.y / spriteSize.y;
        scale.x *= cameraSize.x / spriteSize.x;

        transform.position = Vector2.zero; // Optional
        transform.localScale = scale;
    }

}
