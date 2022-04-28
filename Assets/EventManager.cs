using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    [SerializeField] Vector2 airdropIntervalRange;
    [SerializeField] float nextAirdropTimestamp;
    [SerializeField] GameObject airDropEventPrefab;
    private float elapsedTime; 

    void Start()
    {
        elapsedTime = Time.realtimeSinceStartup;
        nextAirdropTimestamp = elapsedTime + Random.Range(airdropIntervalRange.x, airdropIntervalRange.y);
    }

    void Update()
    {
        elapsedTime = Time.realtimeSinceStartup;

        if (elapsedTime > nextAirdropTimestamp)
        {
            GameObject.Instantiate(airDropEventPrefab);
            nextAirdropTimestamp = elapsedTime + Random.Range(airdropIntervalRange.x, airdropIntervalRange.y);
        }
        
    }
}
