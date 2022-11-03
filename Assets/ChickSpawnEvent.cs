using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickSpawnEvent : Event
{

    [SerializeField] GameObject chuckPrefab;
    private GameObject eggPrefab;
    private GameObject chickensManager;

    private GameObject selectedChicken;
    private GameObject egg;
    private GameObject chick;
    private Vector3 lastEggKnownPosition;
    private Vector3 startingScale;

    [SerializeField] Vector2 chuckTimeRange;

    [ShowOnly] [SerializeField] private bool timeIntervalElapsed;
    [ShowOnly] [SerializeField] private bool lastTrip = false;

    void Start()
    {
        // Get chicken reference
        chickensManager = GameObject.FindGameObjectWithTag("ChickensManager");
        selectedChicken = chickensManager.GetComponent<ChickensManager>().activeChickens[0];
        eggPrefab = chickensManager.GetComponent<ChickensManager>().eggPrefab;

        // Spawn the egg containing the chick
        egg = GameObject.Instantiate(eggPrefab);
        egg.transform.position = selectedChicken.transform.position - new Vector3(0, 0.75f, 0);

        // Start timer
        StartCoroutine(endTimer(Random.Range(chuckTimeRange.x, chuckTimeRange.y)));
    }

    private IEnumerator endTimer(float timerTime)
    {
        yield return new WaitForSeconds(timerTime);
        timeIntervalElapsed = true;
    }

    void Update()
    {
        if(egg == null) // egg destroyed
        {
            if(chick == null) // egg destroyed but chick not spawned -> spawn chick
            {
                chick = GameObject.Instantiate(chuckPrefab);
                chick.transform.position = lastEggKnownPosition;
                chick.transform.parent = this.gameObject.transform;

                // Saving starting parameters
                startingScale = chick.transform.localScale;
            }
            else // chick logic
            {
                moveChuck();
            }

        }
        else
        {
            lastEggKnownPosition = egg.transform.position;
        }
    }


    private void moveChuck()
    {
        if (destinationReached && !timeIntervalElapsed)
        {
            nextDestination = MovementUtilities.generateRandom2DPositionInRange(-xAttackAreaLims, xAttackAreaLims, yAttackAreaLims.x, yAttackAreaLims.y, chick.transform.position.z);
            destinationReached = false;
        }
        else if (destinationReached && timeIntervalElapsed && !lastTrip)
        {

            destinationReached = false;
            lastTrip = true;

            float escapeSide = 1;
            if (chick.transform.position.x > 0)
            {
                escapeSide = -1;
            }
            float x_fixed = escapeSide * xSpawnLims;

            nextDestination = MovementUtilities.generateRandom2DPositionInRange(x_fixed, x_fixed, yAttackAreaLims.x, yAttackAreaLims.y, chick.transform.position.z);

        }
        else if (destinationReached && timeIntervalElapsed && lastTrip)
        {
            Destroy(chick);
            Destroy(gameObject);
        }

        // Move into destination
        Vector3 direction = (chick.transform.position - nextDestination).normalized;
        Vector3 newPosition = chick.transform.position + -direction * speed * Time.smoothDeltaTime;

        float distance = Vector3.Distance(chick.transform.position, nextDestination);
        float stepDistance = Vector3.Distance(chick.transform.position, newPosition);

        if (stepDistance > distance) // then overshoot!
        {
            destinationReached = true;
            chick.transform.position = nextDestination;
        }
        else
        {
            chick.transform.position = newPosition;
        }

        updateSpriteOrientation(direction);
    }

    private void updateSpriteOrientation(Vector3 direction)
    {
        if (direction.x < 0) // Going left
        {
            chick.transform.localScale = new Vector3(startingScale.x * -1, chick.transform.localScale.y, chick.transform.localScale.z);
        }
        else if (direction.x >= 0) // Going right
        {
            chick.transform.localScale = new Vector3(startingScale.x * 1, chick.transform.localScale.y, chick.transform.localScale.z);
        }
    }
}
