using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected Vector2 yAttackAreaLims;
    [SerializeField] protected float xAttackAreaLims;
    [SerializeField] protected float xSpawnLims;
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

        return new Vector3(spawnSide * xSpawnLims, Random.Range(yAttackAreaLims.x, yAttackAreaLims.y), 0);

    }

    protected virtual Vector3 generateNextDestination()
    {
        return new Vector3(Random.Range(-xAttackAreaLims, xAttackAreaLims), Random.Range(yAttackAreaLims.x, yAttackAreaLims.y), 0);
    }
}
