using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggAirdropEvent : MonoBehaviour
{

    [SerializeField] float xPosMoveRange;
    [SerializeField] float xSpawnLimits;
    [SerializeField] Vector2 yLims;
    [SerializeField] bool debug = true;

    private Vector3 startPos;
    private Vector3 endPos;
    private GameObject chicken;
    private Vector3 startingScale;

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
        // Spawn phase & init
        initTime = Time.timeSinceLevelLoad;
        generateTrajectory();
        chicken = GameObject.Instantiate(ChickenPrefab);
        Destroy(chicken.GetComponent<Chicken>()); // main chicken behaviour not required!
        chicken.transform.position = startPos;

        // Movement
        initializeMovement();

        // Eggs drop
        initializeEggDrops();

        // Saving starting parameters
        startingScale = chicken.transform.localScale;

        // Sprite orientation
        setSpriteOrientation(direction);

        // Debugging
        if (debug)
        {
            GameObject start = GameObject.Instantiate(posMarker);
            GameObject end = GameObject.Instantiate(posMarker);

            start.transform.position = startPos;
            end.transform.position = endPos;
        }
    }

    private void initializeMovement()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
        direction = (startPos - endPos).normalized;
        float distance = Vector3.Distance(startPos, endPos);
        runTime = distance / speed;
    }

    private void initializeEggDrops()
    {
        eggsToDrop = (int)Random.Range(eggsRange.x, eggsRange.y);
        spawnInterval = runTime / (eggsToDrop + 1);

        for (int i = 0; i < eggsToDrop; i++)
        {
            spawnTimestamps.Add((i + 1) * spawnInterval);
            Debug.Log(spawnTimestamps[i]);
        }
    }

    private void setSpriteOrientation(Vector3 direction)
    {
        if (direction.x < 0) // Going left
        {
            chicken.transform.localScale = new Vector3(startingScale.x * -1, chicken.transform.localScale.y, chicken.transform.localScale.z);
        }
        else if (direction.x >= 0) // Going right
        {
             chicken.transform.localScale = new Vector3(startingScale.x * 1, chicken.transform.localScale.y, chicken.transform.localScale.z);
        }
    }

    private void generateTrajectory()
    {
        int orientation = -1;
        if (Random.value > 0.5f)
        {
            orientation = 1;
        }

        Vector2 xLims = new Vector2(xPosMoveRange, -xPosMoveRange) * orientation;
        Trajectory trajectory = MovementUtilities.generate2DTrajectoryInRange(xLims, yLims);

        startPos = trajectory.startPoint;
        endPos = trajectory.endPoint;
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
                newEgg.transform.position = chicken.transform.position - new Vector3(0,0.75f,0);

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
        Vector3 newPosition = chicken.transform.position + -direction * speed * Time.smoothDeltaTime;
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
