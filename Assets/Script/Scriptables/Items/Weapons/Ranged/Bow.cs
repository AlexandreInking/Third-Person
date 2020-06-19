using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Bow), menuName = "SO/Weapons/Ranged/Bow")]
public class Bow : PolyCannon
{
    protected override void OnEnable()
    {
        base.OnEnable();

        (liveSlug as Projectile).speed = 1f;
    }
}
