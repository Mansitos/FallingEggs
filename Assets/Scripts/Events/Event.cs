using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    // Basic parameters
    [SerializeField] protected float speed;

    // Positions parameters
    [SerializeField] protected Vector2 yAttackAreaLims;
    [SerializeField] protected float xAttackAreaLims;
    [SerializeField] protected float xSpawnLims;

    // Status Info
    [ShowOnly] [SerializeField] protected Vector3 nextDestination;
    protected Vector3 spawnPosition;
    protected bool destinationReached = false;

    void Start()
    {
    }

    void Update()
    {
    }

    protected virtual Vector3 generateRandomSpawnPosition()
    {
        float spawnSide = 1;
        if (Random.value > 0.5)
        {
            spawnSide = -1;
        }
        float fixed_x = xSpawnLims * spawnSide;
        Vector3 randomSpawnPos = MovementUtilities.generateRandom2DPositionInRange(fixed_x, fixed_x, yAttackAreaLims.x, yAttackAreaLims.y, 0);
        return randomSpawnPos;
    }

    protected virtual Vector3 generateNextDestination()
    {
        Vector3 nextDestination = MovementUtilities.generateRandom2DPositionInRange(-xAttackAreaLims, xAttackAreaLims, yAttackAreaLims.x, yAttackAreaLims.y, 0);
        return nextDestination;
    }
}
