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

    public override void destroy(bool destroyedByPlayer)
    {
        if (destroyedByPlayer)
        {
            base.destroy(destroyedByPlayer);
        }
        else
        {
            if (!manager.devMode) { 
                manager.setLifes(0);
                manager.handleGameOver();
            }
            Destroy(this.gameObject);
        }
    }
}
