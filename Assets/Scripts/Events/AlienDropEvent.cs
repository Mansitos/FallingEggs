using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDropEvent : Event
{
    [SerializeField] GameObject alienShipPrefab;
    [SerializeField] GameObject alienEggPrefab;
    private GameObject alienShip;
    [ShowOnly] [SerializeField] bool isEscaping = false;
    private Vector3 startingScale;
    private bool spawnTimeWaited;
    private float startingSpeed;

    void Start()
    {
        // Spawning phase
        spawnPosition = generateRandomSpawnPosition();

        alienShip = GameObject.Instantiate(alienShipPrefab);
        alienShip.transform.parent = this.gameObject.transform;
        alienShip.transform.position = spawnPosition;

        // Saving starting parameters
        startingScale = alienShip.transform.localScale;
        startingSpeed = speed;

        // Starting destination
        nextDestination = generateNextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        if (alienShip != null)
        {
            if (destinationReached && !isEscaping)
            {
                StartCoroutine(spawnAlienEgg());

                speed = 0;

                destinationReached = false;
                isEscaping = true;

                float escapeSide = 1;
                if (alienShip.transform.position.x > 0)
                {
                    escapeSide = -1;
                }

                nextDestination = new Vector3(escapeSide * xSpawnLims, Random.Range(yAttackAreaLims.x, yAttackAreaLims.y), 0);
            }
            else if (destinationReached && isEscaping)
            {
                Destroy(alienShip);
                Destroy(gameObject);
            }

            // Move into destination
            Vector3 direction = (alienShip.transform.position - nextDestination).normalized;
            Vector3 newPosition = alienShip.transform.position + -direction * speed * Time.deltaTime;

            float distance = Vector3.Distance(alienShip.transform.position, nextDestination);
            float stepDistance = Vector3.Distance(alienShip.transform.position, newPosition);

            if (stepDistance > distance) // then overshoot!
            {
                destinationReached = true;

                alienShip.transform.position = nextDestination;
            }
            else
            {
                alienShip.transform.position = newPosition;
            }

            if (speed > 0)
            {
                // Sprite orientation
                if (direction.x < 0) // Going left
                {
                    alienShip.transform.localScale = new Vector3(startingScale.x * -1, alienShip.transform.localScale.y, alienShip.transform.localScale.z);
                }
                else if (direction.x >= 0) // Going right
                {
                    alienShip.transform.localScale = new Vector3(startingScale.x * 1, alienShip.transform.localScale.y, alienShip.transform.localScale.z);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

    IEnumerator spawnAlienEgg()
    {
        yield return new WaitForSeconds(2);
        GameObject alienEgg = GameObject.Instantiate(alienEggPrefab);
        alienEgg.transform.position = alienShip.transform.position - new Vector3(0, 1, 0);
        speed = startingSpeed;
    }
}
