using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttackEvent : Event
{
    [SerializeField] GameObject eaglePrefab;
    [SerializeField] float escapingSpeedFactor;
    private Vector3 startingScale;

    private GameObject eagle;
    [ShowOnly][SerializeField] bool hasAnEgg = false;
    [ShowOnly][SerializeField] bool isEscaping = false;

    void Start()
    {
        // Spawning & init phase
        spawnPosition = generateRandomSpawnPosition();
        eagle = GameObject.Instantiate(eaglePrefab);
        eagle.transform.parent = this.gameObject.transform;
        eagle.transform.position = spawnPosition;
        nextDestination = generateNextDestination();

        // Saving starting parameters
        startingScale = eagle.transform.localScale;
    }

    // Update is called once per frames
    void Update()
    {
        move();
    }

    private void move()
    {
        if (destinationReached && !hasAnEgg)
        {
            nextDestination = MovementUtilities.generateRandom2DPositionInRange(-xAttackAreaLims, xAttackAreaLims, yAttackAreaLims.x, yAttackAreaLims.y, eagle.transform.position.z);
            destinationReached = false;
        }
        else if(hasAnEgg && destinationReached && !isEscaping)
        {
            destinationReached = false;
            isEscaping = true;
            speed = speed * escapingSpeedFactor;

            float escapeSide = 1;
            if(eagle.transform.position.x > 0)
            {
                escapeSide = -1;
            }
            float x_fixed = escapeSide * xSpawnLims;
            nextDestination = MovementUtilities.generateRandom2DPositionInRange(x_fixed, x_fixed, yAttackAreaLims.x, yAttackAreaLims.y, eagle.transform.position.z);

        }
        else if (hasAnEgg && destinationReached && isEscaping)
        {
            eagle.GetComponent<Eagle>().notifyPenalty();
            Destroy(eagle);
            Destroy(gameObject);
        }

        // Move into destination
        Vector3 direction = (eagle.transform.position - nextDestination).normalized;
        Vector3 newPosition = eagle.transform.position + -direction * speed * Time.smoothDeltaTime;

        float distance = Vector3.Distance(eagle.transform.position, nextDestination);
        float stepDistance = Vector3.Distance(eagle.transform.position, newPosition);

        if (stepDistance > distance) // then overshoot!
        {
            destinationReached = true;
            eagle.transform.position = nextDestination;
        }
        else
        {
            eagle.transform.position = newPosition;
        }

        updateSpriteOrientation(direction);
    }

    private void updateSpriteOrientation(Vector3 direction)
    {
        if (direction.x < 0) // Going left
        {
            eagle.transform.localScale = new Vector3(startingScale.x * -1, eagle.transform.localScale.y, eagle.transform.localScale.z);
        }
        else if (direction.x >= 0) // Going right
        {
            eagle.transform.localScale = new Vector3(startingScale.x * 1, eagle.transform.localScale.y, eagle.transform.localScale.z);
        }
    }

    public void notifyCaptureEgg()
    {
        hasAnEgg = true;
    }

    public bool getHasAnEgg()
    {
        return hasAnEgg;
    }

}
