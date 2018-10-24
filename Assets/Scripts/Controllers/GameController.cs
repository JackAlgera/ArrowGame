using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Vector3[] spawnPositions;
    public GameObject initPos;

    private bool canSpawnArrows = true;
    public float timeBTWSpawns;
    private float currentTimeBTWSpawns = 0;

    public GameObject arrowPrefab;

    public GameObject arrowsInGame;
    public GameObject arrowsToDestroy;


	void Awake () {
		if(instance == null)
        {
            instance = this;
        }

        spawnPositions = new Vector3[initPos.transform.childCount];

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i] = initPos.transform.GetChild(i).transform.position;
        }
	}
	
	void Update () {
        currentTimeBTWSpawns += Time.deltaTime;
        if(currentTimeBTWSpawns >= timeBTWSpawns && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns;
            SpawnArrow();
        }
	}

    public void SpawnArrow()
    {
        Direction randDirect = (Direction)Random.Range(0, sizeof(Direction));
        Vector3 randPos = spawnPositions[Random.Range(0, spawnPositions.Length)];

        GameObject currentArrow = Instantiate(arrowPrefab, randPos, Quaternion.identity);
        currentArrow.transform.parent = arrowsInGame.transform;
        currentArrow.GetComponent<ArrowController>().direc = randDirect;

        switch (randDirect)
        {
            case Direction.Up:
                break;

            case Direction.Down:
                currentArrow.transform.GetChild(0).Rotate(0f, 0f, 180f);
                break;

            case Direction.Left:
                currentArrow.transform.GetChild(0).Rotate(0f, 0f, 90f);
                break;

            case Direction.Right:
                currentArrow.transform.GetChild(0).Rotate(0f, 0f, 270f);
                break;
            default:
                break;
        }
    }

    public void DestroyArrowWithButton(int direc)
    {
        if(arrowsInGame.transform.childCount != 0)
        {
            if ((Direction)direc == arrowsInGame.transform.GetChild(0).GetComponent<ArrowController>().direc)
            {
                DestroyArrow();
            }
        }
    }

    public void DestroyArrow()
    {
        arrowsInGame.transform.GetChild(0).GetComponent<ArrowController>().OnClickDestroy();
        arrowsInGame.transform.GetChild(0).SetParent(arrowsToDestroy.transform);
    }

    public void EndGame()
    {
        StopSpawningArrows();
        foreach(Transform arrow in arrowsInGame.transform)
        {
            arrow.GetComponent<ArrowController>().OnClickDestroy();
        }
    }

    public void StopSpawningArrows()
    {
        canSpawnArrows = false;
    }
}
