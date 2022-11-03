using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Vector2 xlims;
    public Vector2 ylims;
    public float speed;
    private bool destinationReached = false;
    private Vector3 destination;
    private Vector3 startingScale;
    private Manager manager;
    public bool spawnPhase = true;
    private float initTime;

    void Start()
    {
        initTime = Time.timeSinceLevelLoad;
        startingScale = transform.localScale;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

        // First destination: from spawning location to first in-game-area location
        destination = generateNextDestination();
    }

    void Update()
    {
        if (destinationReached) // new destination required
        {
            destination = generateNextDestination();
            spawnPhase = false; // become false when destinationReached == true for the 1st time; after 1st time remains false forever!
            destinationReached = false;
        }

        // Move into destination logic:

        Vector3 direction = (transform.position - destination).normalized;

        updateSpriteOrientation(direction);

        Vector3 newPosition = transform.position + -direction * speed * (2.0f - manager.getDifficultyFactor(initTime)) * Time.smoothDeltaTime;

        float remainingDistance = Vector3.Distance(transform.position, destination);
        float stepDistance = Vector3.Distance(transform.position, newPosition);

        if(stepDistance > remainingDistance) // if true -> overshooting -> dest. reached
        {
            destinationReached = true;
            transform.position = destination;
        }
        else
        {
            transform.position = newPosition;
        }
    }

    private void updateSpriteOrientation(Vector3 direction)
    {
        if (direction.x < 0) // Going left
        { 
            transform.localScale = new Vector3(startingScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x >= 0) // Going right
        {
            transform.localScale = new Vector3(startingScale.x * 1, transform.localScale.y, transform.localScale.z);
        }
    }

    private Vector3 generateNextDestination()
    {
        return MovementUtilities.generateRandom2DPositionInRange(xlims.x, xlims.y, ylims.x, ylims.y, this.transform.position.z);
    }
}
