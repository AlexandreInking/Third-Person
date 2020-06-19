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


    public override void Collided(Collision collision)
    {
        base.Collided(collision);

        rBody.constraints = RigidbodyConstraints.FreezeAll;

        transform.SetParent(collision.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TriggerImpact(collision);

        //Debug.LogError(collision.transform);
    }
}
