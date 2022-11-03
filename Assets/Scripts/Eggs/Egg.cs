using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Entity
{
    public Vector2 speedRange;
    protected float speed;

    protected override void Start()
    {
        base.Start();
        speed = Random.Range(speedRange.x, speedRange.y);
    }

    void Update()
    {
        move();
    }

    protected virtual void move()
    {
        transform.position = transform.position - new Vector3(0, speed, 0) * Time.smoothDeltaTime;
    }
}
