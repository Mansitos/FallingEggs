using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEgg : Egg
{

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public override void registerHit()
    {
        remainingHits--;
        if (remainingHits <= 0)
        {
            destroy(true);
        }
    }

    private void move()
    {
        transform.position = transform.position - new Vector3(0, speed, 0) * Time.deltaTime;
    }

    public override void destroy(bool destroyedByPlayer)
    {
        if (destroyedByPlayer)
        {
            base.destroy(destroyedByPlayer);
        }
        else
        {
            manager.setLifes(0);
            manager.gameOver();
            Destroy(this.gameObject);
        }
    }
}
