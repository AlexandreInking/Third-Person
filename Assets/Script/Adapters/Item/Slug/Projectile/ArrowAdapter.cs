using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAdapter : ProjectileAdapter
{
    public override void Initialize()
    {
        base.Initialize();

        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }


    public override void Collided()
    {
        base.Collided();

        rBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TriggerImpact();

        //Debug.LogError(collision.transform);
    }
}
