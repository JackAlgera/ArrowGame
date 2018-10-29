using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour {

    public GameObject arrowPrefab;
    public GameObject Parent;

    public void PressButton()
    {
        SpawnArrow();
    }

    public void SpawnArrow()
    {
        GameObject spawnedArrow = Instantiate(arrowPrefab);
        spawnedArrow.transform.SetParent(transform.parent);
        spawnedArrow.transform.localPosition = transform.localPosition;
        spawnedArrow.transform.localScale = transform.localScale;
        spawnedArrow.transform.SetParent(Parent.transform);
    }
}
