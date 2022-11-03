using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLifeEgg : Egg
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
            manager.addLife();
            destroy(true);
        }
    }
}
