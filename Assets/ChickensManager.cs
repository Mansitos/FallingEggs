using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickensManager : MonoBehaviour
{
    private Manager manager;
    [SerializeField] float nextChickenInterval;
    private List<GameObject> chickens = new List<GameObject>();
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] float increaseFactor;

    [SerializeField] Vector2 eggSpawnInterval;
    private Vector2 startingEggSpawnInterval;

    private float initTime;

    [ShowOnly] public int spawnedChickens = 0;
    [ShowOnly] [SerializeField] float nextSpawnTimestamp;
    [SerializeField] List<GameObject> activeChickens = new List<GameObject>();

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

    private bool waitForNextSpawn = false;
    private Texture chickenSprite;



    void Start()
    {
        startingEggSpawnInterval = eggSpawnInterval;
        startingBadEggChance = badEggChance;
        startingStrongEggChance = strongEggChance;
        startingNewLifeChange = newLifeChance;
        startingGoldEggChance = goldEggChance;

        initTime = Time.timeSinceLevelLoad;

        nextSpawnTimestamp = -1;
        activeChickens.Clear();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > nextSpawnTimestamp)
        {
            spawnedChickens++;
            nextSpawnTimestamp = Time.timeSinceLevelLoad + nextChickenInterval;
            nextChickenInterval = nextChickenInterval * increaseFactor;
            GameObject spawnedChicken = GameObject.Instantiate(chickenPrefab);
            spawnedChicken.GetComponent<Renderer>().material.SetTexture("_MainTex", chickenSprite);
            activeChickens.Add(spawnedChicken);
        }

        if (waitForNextSpawn == false)
        {
            StartCoroutine(SpawnEgg());
            waitForNextSpawn = true;
        }

    }

    public void setChickensSprite(Texture sprite)
    {
        chickenSprite = sprite;
    }


    IEnumerator SpawnEgg()
    {
        badEggChance = startingBadEggChance * (2.0f - manager.getDifficultyFactor(initTime));
        strongEggChance = startingStrongEggChance * (2.0f - manager.getDifficultyFactor(initTime));
        newLifeChance = startingNewLifeChange * manager.getDifficultyFactor(initTime);

        GameObject newEgg;

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

        // Randomly select a chicken
        int index = (int)Random.Range(0, spawnedChickens);


        newEgg.transform.position = activeChickens[index].transform.position - new Vector3(0, 0.5f, 0);

        eggSpawnInterval = startingEggSpawnInterval * manager.getDifficultyFactor(initTime);
        float waitTime = Random.Range(eggSpawnInterval.x, eggSpawnInterval.y);
        yield return new WaitForSeconds(waitTime);
        waitForNextSpawn = false;
    }

}
