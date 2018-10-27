using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SpawnMethods { Random, Burst, LeftToRight, Packs, TwoDirections, DirectionDepLocation}

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Vector3[] spawnPositions;
    public GameObject initPos;
    private bool canSpawnArrows = true;
    public float spawnRate = 1;
    private float timeBTWSpawns;
    private float currentTimeBTWSpawns = 0;

    // Change spawn method
    public float timeBTWSpawnChanges = 5f;
    public float currentTimeBTWSpanChanges = 0;

    public GameObject[] arrowPrefab;
    public float arrowSpeed;
    public float mawArrowSpeed;
    public float arrowSpeedIncrease;

    public GameObject arrowsInGame;
    public GameObject arrowsToDestroy;

    public int score;
    public Text scoreText;

    public SpawnMethods currentSpawnMethod;

    // Left to Right spawn
    private bool spawnLeft = true;
    private int spawnLocationId = 0;
    private int spawnDirectionOfTravel = 1;
    // Packs
    private int nbrOfArrowsInPack = 2;
    private int nbrOfArrowsSpawned = 0;
    private Direction direcOfArrow = Direction.Left;
    // Only two directions;
    private bool leftAndRight;

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

        UpdateScore(false);
        ChangeSpawnSettings();
        timeBTWSpawns = arrowSpeed * 5f;

        if(Random.Range(0,2) == 1)
        {
            leftAndRight = false;
        }
        else
        {
            leftAndRight = true;
        }
	}
	
	void Update () {

        switch (currentSpawnMethod)
        {
            case SpawnMethods.Random:
                SpawnRandom();
                break;

            case SpawnMethods.Burst:
                SpawnBurst();
                break;

            case SpawnMethods.LeftToRight:
                SpawnLeftToRight();
                break;

            case SpawnMethods.Packs:
                SpawnPacks();
                break;

            case SpawnMethods.TwoDirections:
                SpawnTwoDirections();
                break;

            case SpawnMethods.DirectionDepLocation:
                SpawnDirectionDependant();
                break;

            default:
                break;
        }

        if(currentTimeBTWSpanChanges > timeBTWSpawnChanges)
        {
            currentTimeBTWSpanChanges = 0f;
            ChangeSpawnSettings();
        }

        currentTimeBTWSpanChanges += Time.deltaTime;
        currentTimeBTWSpawns += Time.deltaTime;
        IncreaseSpeed();
	}

    public void ChangeSpawnSettings()
    {
        currentSpawnMethod = (SpawnMethods)Random.Range(0, sizeof(SpawnMethods));
        switch (currentSpawnMethod)
        {
            case SpawnMethods.Random:
                spawnRate = 1f;
                timeBTWSpawnChanges = 10f;
                break;

            case SpawnMethods.Burst:
                spawnRate = 0.82f;
                spawnLocationId = Random.Range(0, spawnPositions.Length);
                timeBTWSpawnChanges = 5f;
                break;

            case SpawnMethods.LeftToRight:
                spawnRate = 1f;
                spawnLeft = !spawnLeft;
                timeBTWSpawnChanges = 10f;
                if (spawnLeft)
                {
                    spawnLocationId = 0;
                    spawnDirectionOfTravel = 1;
                }
                else
                {
                    spawnLocationId = spawnPositions.Length - 1;
                    spawnDirectionOfTravel = -1;
                }
                break;

            case SpawnMethods.Packs:
                timeBTWSpawnChanges = 10f;
                nbrOfArrowsInPack = Random.Range(2, 6);
                nbrOfArrowsSpawned = 0;
                direcOfArrow = (Direction)Random.Range(0, sizeof(Direction) - 1);
                spawnLocationId = Random.Range(0, spawnPositions.Length);
                break;

            case SpawnMethods.TwoDirections:
                timeBTWSpawnChanges = 8f;
                leftAndRight = !leftAndRight;
                break;

            case SpawnMethods.DirectionDepLocation:
                timeBTWSpawnChanges = 10f;
                spawnRate = 1f;
                break;

            default:
                break;
        }
    }

    public void SpawnRandom()
    {
        if (currentTimeBTWSpawns >= (timeBTWSpawns * spawnRate) && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns * spawnRate;
            Vector3 randPos = spawnPositions[Random.Range(0, spawnPositions.Length)];
            Direction direc = (Direction)Random.Range(0, 4);
            SpawnArrow(randPos, direc);
        }
    }

    public void SpawnBurst()
    {
        if (currentTimeBTWSpawns >= (timeBTWSpawns * spawnRate) && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns * spawnRate;
            Vector3 pos = spawnPositions[spawnLocationId];
            Direction direc = (Direction)Random.Range(0, 4);
            SpawnArrow(pos, direc);
        }
    }

    public void SpawnLeftToRight()
    {
        if (currentTimeBTWSpawns >= (timeBTWSpawns * spawnRate) && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns * spawnRate;

            Vector3 pos = spawnPositions[spawnLocationId];
            Direction direc = (Direction)Random.Range(0, 4);
            SpawnArrow(pos, direc);

            if (spawnLocationId == spawnPositions.Length - 1)
            {
                spawnDirectionOfTravel = -1;
            }
            else if(spawnLocationId == 0)
            {
                spawnDirectionOfTravel = 1;
            }
            spawnLocationId += spawnDirectionOfTravel;
        }
    }

    public void SpawnPacks()
    {
        if (currentTimeBTWSpawns >= (timeBTWSpawns * spawnRate) && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns * spawnRate;

            nbrOfArrowsSpawned += 1;
            Vector3 pos = spawnPositions[spawnLocationId];
            SpawnArrow(pos, direcOfArrow);

            if (nbrOfArrowsSpawned >= nbrOfArrowsInPack)
            {
                nbrOfArrowsSpawned = 0;
                direcOfArrow = (Direction)Random.Range(0, sizeof(Direction) - 1);

                int newSpawnLocationId= Random.Range(0, spawnPositions.Length);
                while (newSpawnLocationId == spawnLocationId)
                {
                    newSpawnLocationId = Random.Range(0, spawnPositions.Length);
                }
                spawnLocationId = newSpawnLocationId;
            }
        }
    }

    public void SpawnTwoDirections()
    {
        if (currentTimeBTWSpawns >= (timeBTWSpawns * spawnRate) && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns * spawnRate;
            if (leftAndRight)
            {
                if (Random.Range(0, 2) == 1)
                {
                    spawnLocationId = spawnPositions.Length - 1;
                    direcOfArrow = Direction.Right;
                }
                else
                {
                    spawnLocationId = 0;
                    direcOfArrow = Direction.Left;
                }
                /*
                if (Random.Range(0, 2) == 1)
                {
                    direcOfArrow = Direction.Right;
                }
                else
                {
                    direcOfArrow = Direction.Left;
                }
                */
            }
            else
            {
                if (Random.Range(0, 2) == 1)
                {
                    spawnLocationId = spawnPositions.Length - 2;
                    direcOfArrow = Direction.Down;
                }
                else
                {
                    spawnLocationId = 1;
                    direcOfArrow = Direction.Up;
                }
            }

            SpawnArrow(spawnPositions[spawnLocationId], direcOfArrow);
        }
    }

    public void SpawnDirectionDependant()
    {
        if (currentTimeBTWSpawns >= (timeBTWSpawns * spawnRate) && canSpawnArrows)
        {
            currentTimeBTWSpawns -= timeBTWSpawns * spawnRate;
            spawnLocationId = Random.Range(1, spawnPositions.Length - 1);
            Direction direc = (Direction)Random.Range(0, 4);

            switch (spawnLocationId)
            {
                case 1:
                    direc = Direction.Left;
                    break;

                case 2:
                    direc = Direction.Up;
                    break;

                case 3:
                    direc = Direction.Down;
                    break;

                case 4:
                    direc = Direction.Right;
                    break;

                default:
                    break;
            }

            Vector3 pos = spawnPositions[spawnLocationId];
            SpawnArrow(pos, direc);
        }
    }

    public void IncreaseSpeed()
    {
        if(arrowSpeed < mawArrowSpeed)
        {
            arrowSpeed += arrowSpeedIncrease * Time.deltaTime;
            timeBTWSpawns = 1.5f / arrowSpeed;
        }
    }

    public void UpdateScore(bool increaseScore)
    {
        if(increaseScore)
        {
            score++;
            if (((int)score % 10) == 0)
            {
                scoreText.GetComponent<ScoreShaker>().Shake();
            }
        }
        scoreText.text = "" + score;
    }

    public void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void SpawnArrow(Vector3 spawnPos, Direction direc)
    {
        GameObject currentArrow = Instantiate(arrowPrefab[(int)direc], spawnPos, Quaternion.identity);
        currentArrow.transform.parent = arrowsInGame.transform;
        currentArrow.GetComponent<ArrowController>().direc = direc;
        currentArrow.GetComponent<ArrowController>().speed = arrowSpeed;

        switch (direc)
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
                UpdateScore(true);
                DestroyArrow();
            }
            else
            {
                EndGame();
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
        UpdateHighScore();
    }

    public void StopSpawningArrows()
    {
        canSpawnArrows = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        UpdateHighScore();
        SceneManager.LoadScene("MainMenu");
    }
}
