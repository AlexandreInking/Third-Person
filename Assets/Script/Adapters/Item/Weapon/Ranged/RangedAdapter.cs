using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAdapter : WeaponAdapter
{
    [Tooltip("Currently Inside Clip")]
    [HideInInspector] public int clipCount = 0;

    [Space]
    [Tooltip("Slug Exit Point")]
    public Transform muzzle;

    public override void Equip()
    {
        base.Equip();

        transform.SetParent(inventory.leftHand);
    }

    public override void UnEquip()
    {
        base.UnEquip();

        transform.SetParent(inventory.rangedWeapon);
    }
}
