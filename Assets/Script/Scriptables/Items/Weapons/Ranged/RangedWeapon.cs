using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Space]
    [Tooltip("Current Weapon Barrel")]
    public Barrel liveBarrel;

    Volume volume;
    RangedAdapter adapter;

    public override void Attack(Character attacker)
    {
        volume = ((attacker.profile as CombatantProfile).inventory as PlayerInventory).ActiveEntry.Value;

        adapter = volume.Adapter as RangedAdapter;

        for (int i = 0; i < adapter.chamberCount; i++)
        {
            if (liveBarrel.liveSlug is Projectile)
            {
                FireProjectile(attacker);
            }

            adapter.chamberCount--;
        }

        adapter.InstantLoad();
    }

    void FireProjectile(Character shooter)
    {
        Projectile projectile = liveBarrel.liveSlug as Projectile;

        Vector3 aimPosition = shooter.controllerPack.GetController<CombatController>().aimPosition;

        Vector3 aimDirection = (aimPosition - adapter.muzzle.position).normalized;

        //If Projectile
        GameObject slug = Instantiate(projectile.itemPrefab, adapter.muzzle.position,
            Quaternion.LookRotation(aimDirection));

        UnGrabbableAdapter slugAdapter = slug.GetComponent<Adapter>() as UnGrabbableAdapter;

        slugAdapter.Actor = shooter;

        slugAdapter.Flavor = projectile.flavor;

        slug.GetComponent<Rigidbody>().velocity = aimDirection * projectile.range * projectile.speed;
    }

    void FireBeam()
    {

    }
}
