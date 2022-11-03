using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : Entity
{
    private ChickSpawnEvent chickManager;

    protected override void Start()
    {
        base.Start();
        chickManager = GetComponentInParent<ChickSpawnEvent>();
    }

    void Update()
    {
    }

    public override void destroy(bool destroyedByPlayer)
    {
        Destroy(chickManager);
        base.destroy(destroyedByPlayer);
    }

}
