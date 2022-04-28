using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Vector2 xlims;
    public Vector2 ylims;
    public float speed;
    public Vector2 spawnInterval;
    public GameObject eggPrefab;

    public float badEggChance;
    private float startingBadEggChance;
    public GameObject badEggPrefab;

    public float strongEggChance;
    private float startingStrongEggChance;
    public GameObject strongEggPrefab;

    public float newLifeChance;
    private float startingNewLifeChange;
    public GameObject newLifePrefab;

    private bool facingLeft = true;
    private bool destinationReached = true;
    private Vector3 destination;

    private bool waitForNextSpawn = false;
    private Vector2 startingSpawnInterval;
    private Manager manager;

    void Start()
    {
        startingSpawnInterval = spawnInterval;
        startingBadEggChance = badEggChance;
        startingStrongEggChance = strongEggChance;
        startingNewLifeChange = newLifeChance;

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        

        if (destinationReached)
        {
            destination = new Vector3(Random.Range(xlims.x, xlims.y), Random.Range(ylims.x, ylims.y));
            destinationReached = false;
        }

        // Move into destination
        Vector3 direction = (transform.position - destination).normalized;
        Vector3 newPosition = transform.position + -direction * speed * (2.0f - manager.getDifficultyFactor()) * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, destination);
        float stepDistance = Vector3.Distance(transform.position, newPosition);

        if(stepDistance > distance) // then overshoot!
        {
            destinationReached = true;
            transform.position = destination;
        }
        else
        {
            transform.position = newPosition;
        }

        if(waitForNextSpawn == false)
        {
            StartCoroutine(SpawnEgg());
            waitForNextSpawn = true;
        }
    }

    IEnumerator SpawnEgg()
    {
        badEggChance = startingBadEggChance * (2.0f - manager.getDifficultyFactor());
        strongEggChance = startingStrongEggChance * (2.0f - manager.getDifficultyFactor());
        newLifeChance = startingNewLifeChange * manager.getDifficultyFactor();

        GameObject newEgg;

        if (Random.value < badEggChance)
        {
            newEgg = Object.Instantiate(badEggPrefab);
        }
        else if (Random.value < newLifeChance && !manager.isMaxLife())
        {
            newEgg = Object.Instantiate(newLifePrefab);
        }
        else if(Random.value < strongEggChance)
        {
            newEgg = Object.Instantiate(strongEggPrefab);
        }
        else
        {
            newEgg = Object.Instantiate(eggPrefab);
        }
        newEgg.transform.position = this.transform.position - new Vector3(0, 0.5f, 0);

        spawnInterval = startingSpawnInterval * manager.getDifficultyFactor();
        float waitTime = Random.Range(spawnInterval.x, spawnInterval.y);
        yield return new WaitForSeconds(waitTime);
        waitForNextSpawn = false;
    }
}
