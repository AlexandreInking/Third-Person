using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrowAdapter : ArrowAdapter
{
    public override void Collided(Collision collision)
    {
        base.Collided(collision);

        ExplosionFlavor flavor = Flavor as ExplosionFlavor;

        flavor.Explode(transform);
    }
}
