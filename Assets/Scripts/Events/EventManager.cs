using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject airDropEventPrefab;
    [SerializeField] Vector2 airdropIntervalRange;
    [SerializeField] float firstAirdropEventTimestampOffset;
    [ShowOnly] [SerializeField] float nextAirdropTimestamp;

    [SerializeField] GameObject eagleAttackEventPrefab;
    [SerializeField] Vector2 eagleAttackIntervalRange;
    [SerializeField] float firstEagleAttackEventTimestampOffset;
    [ShowOnly] [SerializeField] float nextEagleAttackTimestamp;

    [SerializeField] GameObject alienAttackEventPrefab;
    [SerializeField] Vector2 alienAttackIntervalRange;
    [SerializeField] float firstAlienAttackEventTimestampOffset;
    [ShowOnly] [SerializeField] float nextAlienAttackTimestamp;

    [SerializeField] GameObject chuckAttackEventPrefab;
    [SerializeField] Vector2 chuckAttackIntervalRange;
    [SerializeField] float firstChuckAttackEventTimestampOffset;
    [ShowOnly] [SerializeField] float nextChuckAttackTimestamp;

    private float elapsedTime; 

    void Start()
    {
        elapsedTime = Time.realtimeSinceStartup;

        nextAirdropTimestamp     = firstAirdropEventTimestampOffset + elapsedTime + Random.Range(airdropIntervalRange.x, airdropIntervalRange.y);
        nextEagleAttackTimestamp = firstEagleAttackEventTimestampOffset + elapsedTime + Random.Range(eagleAttackIntervalRange.x, eagleAttackIntervalRange.y);
        nextAlienAttackTimestamp = firstAlienAttackEventTimestampOffset + elapsedTime + Random.Range(alienAttackIntervalRange.x, alienAttackIntervalRange.y);
        nextChuckAttackTimestamp = firstChuckAttackEventTimestampOffset + elapsedTime + Random.Range(chuckAttackIntervalRange.x, chuckAttackIntervalRange.y);
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

        if (elapsedTime > nextChuckAttackTimestamp)
        {
            GameObject.Instantiate(chuckAttackEventPrefab);
            nextChuckAttackTimestamp = elapsedTime + Random.Range(chuckAttackIntervalRange.x, chuckAttackIntervalRange.y);
        }
    }
}
