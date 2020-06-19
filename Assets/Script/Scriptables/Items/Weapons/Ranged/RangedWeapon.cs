using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Space]
    [Tooltip("Current Weapon Barrel")]
    public Barrel liveBarrel;

    public override void Attack(Character attacker)
    {
        if (liveBarrel.liveSlug is Projectile)
        {
            FireProjectile(attacker);
        }
    }

    void FireProjectile(Character shooter)
    {
        Volume volume = ((shooter.profile as CombatantProfile).inventory as PlayerInventory).ActiveEntry.Value;

        RangedAdapter adapter = volume.Adapter as RangedAdapter;

        Projectile projectile = liveBarrel.liveSlug as Projectile;

        Vector3 aimPosition = shooter.controllerPack.GetController<CombatController>().aimPosition;

        Vector3 aimDirection = (aimPosition - adapter.muzzle.position).normalized;

        //If Projectile
        GameObject slug = Instantiate(projectile.itemPrefab, adapter.muzzle.position,
            Quaternion.LookRotation(aimDirection));

        slug.GetComponent<Adapter>().Actor = shooter;

        slug.GetComponent<Rigidbody>().velocity = aimDirection * projectile.range * projectile.speed;
    }

    void FireBeam()
    {

    }
}
