using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggAirdropEvent : MonoBehaviour
{

    [SerializeField] float xPosMoveRange;
    [SerializeField] float xSpawnLimits;
    [SerializeField] Vector2 yPos;
    [SerializeField] bool debug = true;

    private Vector3 startPos;
    private Vector3 endPos;
    private GameObject chicken;


    [SerializeField] Vector2 speedRange;
    [SerializeField] Vector2 eggsRange;

    private float speed;
    private int eggsToDrop;
    private Vector3 direction;

    [SerializeField] GameObject posMarker;
    [SerializeField] GameObject ChickenPrefab;
    [SerializeField] GameObject eggPrefab;

    private float runTime;
    private float spawnInterval;
    private int spawnedEggs = 0;

    private float initTime;
    private List<float> spawnTimestamps = new List<float>();


    void Start()
    {
        initTime = Time.timeSinceLevelLoad;

        int orientation = -1;
        if(Random.value > 0.5f)
        {
            orientation = 1;
        }
        startPos = new Vector3(orientation*xPosMoveRange, Random.Range(yPos.x, yPos.y), 0);
        endPos  = new Vector3(orientation*-xPosMoveRange, Random.Range(yPos.x, yPos.y), 0);

        if (debug)
        {
            GameObject start = GameObject.Instantiate(posMarker);
            GameObject end = GameObject.Instantiate(posMarker);

            start.transform.position = startPos;
            end.transform.position = endPos;
        }

        chicken = GameObject.Instantiate(ChickenPrefab);
        Destroy(chicken.GetComponent<Chicken>());
        
        chicken.transform.position = startPos;

        speed = Random.Range(speedRange.x, speedRange.y);
        eggsToDrop = (int)Random.Range(eggsRange.x, eggsRange.y);

        direction = (startPos - endPos).normalized;

        float distance = Vector3.Distance(startPos, endPos);
        runTime = distance/speed;
        spawnInterval = runTime / (eggsToDrop+1);

        for(int i = 0; i<eggsToDrop; i++)
        {
            spawnTimestamps.Add((i+1) * spawnInterval);
            Debug.Log(spawnTimestamps[i]);
        }

        Debug.Log("____");
        Debug.Log(eggsToDrop);
        Debug.Log(distance);
        Debug.Log(runTime);
        //Debug.Log(runTime);
        //Debug.Log(spawnInterval);

    }

    void Update()
    {
        move();
        spawnEggs();
    }

    private void spawnEggs()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initTime;
        for(int i = 0; i < eggsToDrop; i++)
        {
            if(timeSinceInitialization >= spawnTimestamps[i] && spawnedEggs <= i)
            {
                spawnedEggs++;
                GameObject newEgg = GameObject.Instantiate(eggPrefab);
                newEgg.transform.position = chicken.transform.position - new Vector3(0,0.5f,0);

                if(newEgg.transform.position.x < -xSpawnLimits || newEgg.transform.position.x > xSpawnLimits)
                {
                    Debug.LogWarning("Egg from airdrop deleted because spawned outside screen limits!");
                    Destroy(newEgg);
                }
            }
        }
    }

    private void move()
    {
        Vector3 newPosition = chicken.transform.position + -direction * speed * Time.deltaTime;
        float distance = Vector3.Distance(chicken.transform.position, endPos);
        float stepDistance = Vector3.Distance(chicken.transform.position, newPosition);
        chicken.transform.position = newPosition;

        if(stepDistance > distance)
        {
            Destroy(chicken);
            Destroy(gameObject);
        }
    }
}
