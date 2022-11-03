using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickensManager : MonoBehaviour
{
    private Manager manager;
    [SerializeField] float nextChickenSpawnInterval;
    private List<GameObject> chickens = new List<GameObject>();
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] float increaseFactor;

    [SerializeField] Vector2 nextEggSpawnInterval;
    private Vector2 startingEggSpawnInterval;

    [SerializeField] protected float xSpawnLims;
    [SerializeField] protected Vector2 ySpawnLims;

    private float initTime;

    [ShowOnly] public int spawnedChickens = 0;
    [ShowOnly] [SerializeField] float nextChickenSpawnTimestamp;
    [SerializeField] public List<GameObject> activeChickens = new List<GameObject>();

    public GameObject eggPrefab;

    public float badEggChance;
    private float startingBadEggChance;
    public GameObject badEggPrefab;

    public float strongEggChance;
    private float startingStrongEggChance;
    public GameObject strongEggPrefab;

    public float goldEggChance;
    private float startingGoldEggChance;
    public GameObject goldEggPrefab;

    public float newLifeChance;
    private float startingNewLifeChange;
    public GameObject newLifePrefab;

    private bool waitForNextEggSpawn = false;
    private Texture chickenSprite;



    void Start()
    {
        startingEggSpawnInterval = nextEggSpawnInterval;
        startingBadEggChance = badEggChance;
        startingStrongEggChance = strongEggChance;
        startingNewLifeChange = newLifeChance;
        startingGoldEggChance = goldEggChance;

        initTime = Time.timeSinceLevelLoad;

        nextChickenSpawnTimestamp = -1;
        activeChickens.Clear();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        // Wait-time for next chicken spawn is elapsed...
        if (Time.timeSinceLevelLoad > nextChickenSpawnTimestamp) 
        {
            spawnChicken();
        }

        // Wait-time for next egg spawn is elapsed...
        if (waitForNextEggSpawn == false)
        {
            StartCoroutine(SpawnEgg());
            waitForNextEggSpawn = true;
        }

    }

    private void spawnChicken()
    { 
        nextChickenSpawnTimestamp = Time.timeSinceLevelLoad + nextChickenSpawnInterval;
        nextChickenSpawnInterval = nextChickenSpawnInterval * increaseFactor;

        GameObject spawnedChicken = GameObject.Instantiate(chickenPrefab);

        updateChickenSprite(spawnedChicken);

        Vector3 spawnPosition = generateRandomSpawnPosition();

        spawnedChicken.transform.position = spawnPosition;

        spawnedChickens++;
        activeChickens.Add(spawnedChicken);
    }

    IEnumerator SpawnEgg()
    {
        badEggChance = startingBadEggChance * (2.0f - manager.getDifficultyFactor(initTime));
        strongEggChance = startingStrongEggChance * (2.0f - manager.getDifficultyFactor(initTime));
        newLifeChance = startingNewLifeChange * manager.getDifficultyFactor(initTime);

        GameObject newEgg;

        // Randomly select a chicken
        int selectedChickenIndex = (int)Random.Range(0, spawnedChickens);
        GameObject selectedChicken = activeChickens[selectedChickenIndex];

        bool isSelectedChickenInSpawnPhase = selectedChicken.GetComponent<Chicken>().spawnPhase;

        if (isSelectedChickenInSpawnPhase == false && selectedChicken != null)
        {
            if (Random.value < badEggChance)
            {
                newEgg = Object.Instantiate(badEggPrefab);
            }
            else if (Random.value < newLifeChance && !manager.isMaxLife())
            {
                newEgg = Object.Instantiate(newLifePrefab);
            }
            else if (Random.value < strongEggChance)
            {
                newEgg = Object.Instantiate(strongEggPrefab);
            }
            else if (Random.value < goldEggChance)
            {
                newEgg = Object.Instantiate(goldEggPrefab);
            }
            else
            {
                newEgg = Object.Instantiate(eggPrefab);
            }

            newEgg.transform.position = selectedChicken.transform.position - new Vector3(0, 0.75f, 0);
            nextEggSpawnInterval = startingEggSpawnInterval * manager.getDifficultyFactor(initTime);
            float waitTime = Random.Range(nextEggSpawnInterval.x, nextEggSpawnInterval.y);
            yield return new WaitForSeconds(waitTime);
            waitForNextEggSpawn = false;
        }
        else //Selected chicken is in spawn phase, select another chicken in next frame and don't wait for next spawn
        {
            yield return new WaitForSeconds(0.2f);
            waitForNextEggSpawn = false;
        }
    }


    protected virtual Vector3 generateRandomSpawnPosition()
    {
        float spawnSide = 1;
        if (Random.value > 0.5)
        {
            spawnSide = -1;
        }
        float fixed_x = xSpawnLims * spawnSide;
        Vector3 randomSpawnPos = MovementUtilities.generateRandom2DPositionInRange(fixed_x, fixed_x, ySpawnLims.x, ySpawnLims.y, 0);
        return randomSpawnPos;
    }

    private void updateChickenSprite(GameObject chicken)
    {
        chicken.GetComponent<Renderer>().material.SetTexture("_MainTex", chickenSprite);
    }

    public void setSelectedChickenSprite(Texture sprite)
    {
        chickenSprite = sprite;
    }

}
