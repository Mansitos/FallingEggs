using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Vector2 xlims;
    public Vector2 ylims;
    public float speed;
    private bool facingLeft = true;
    private bool destinationReached = true;
    private Vector3 destination;
    private Vector3 startingScale;

    private Manager manager;

    private bool flipped = false;

    private float initTime;

    void Start()
    {

        initTime = Time.timeSinceLevelLoad;
        startingScale = transform.localScale;

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        

        if (destinationReached)
        {
            destination = new Vector3(Random.Range(xlims.x, xlims.y), Random.Range(ylims.x, ylims.y));
            destinationReached = false;
        }

        // Move into destination
        Vector3 direction = (transform.position - destination).normalized;

        if(direction.x < 0) { // Going left
            transform.localScale = new Vector3(startingScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if(direction.x >= 0) // Going right
        {
            transform.localScale = new Vector3(startingScale.x * 1, transform.localScale.y, transform.localScale.z);
        }

        Vector3 newPosition = transform.position + -direction * speed * (2.0f - manager.getDifficultyFactor(initTime)) * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, destination);
        float stepDistance = Vector3.Distance(transform.position, newPosition);

        if(stepDistance > distance) // then overshoot!
        {
            destinationReached = true;
            transform.position = destination;
        }
        else
        {
            transform.position = newPosition;
        }
    }
}
