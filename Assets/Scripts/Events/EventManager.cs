using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject airDropEventPrefab;
    [SerializeField] Vector2 airdropIntervalRange;
    [ShowOnly] [SerializeField] float nextAirdropTimestamp;

    [SerializeField] GameObject eagleAttackEventPrefab;
    [SerializeField] Vector2 eagleAttackIntervalRange;
    [ShowOnly] [SerializeField] float nextEagleAttackTimestamp;

    [SerializeField] GameObject alienAttackEventPrefab;
    [SerializeField] Vector2 alienAttackIntervalRange;
    [ShowOnly] [SerializeField] float nextAlienAttackTimestamp;

    private float elapsedTime; 

    void Start()
    {
        elapsedTime = Time.realtimeSinceStartup;

        nextAirdropTimestamp     = elapsedTime + Random.Range(airdropIntervalRange.x, airdropIntervalRange.y);
        nextEagleAttackTimestamp = elapsedTime + Random.Range(eagleAttackIntervalRange.x, eagleAttackIntervalRange.y);
        nextAlienAttackTimestamp = elapsedTime + Random.Range(alienAttackIntervalRange.x, alienAttackIntervalRange.y);
    }

    void Update()
    {
        elapsedTime = Time.realtimeSinceStartup;

        if (elapsedTime > nextAirdropTimestamp)
        {
            GameObject.Instantiate(airDropEventPrefab);
            nextAirdropTimestamp = elapsedTime + Random.Range(airdropIntervalRange.x, airdropIntervalRange.y);
        }

        if(elapsedTime > nextEagleAttackTimestamp)
        {
            GameObject.Instantiate(eagleAttackEventPrefab);
            nextEagleAttackTimestamp = elapsedTime + Random.Range(eagleAttackIntervalRange.x, eagleAttackIntervalRange.y);
        }

        if (elapsedTime > nextAlienAttackTimestamp)
        {
            GameObject.Instantiate(alienAttackEventPrefab);
            nextAlienAttackTimestamp = elapsedTime + Random.Range(alienAttackIntervalRange.x, alienAttackIntervalRange.y);
        }
    }
}
