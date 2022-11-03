using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenEgg : Egg
{
    [SerializeField] int goldValue;

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
            manager.collectCoin(goldValue);
            destroy(true);
        }
    }
}
