using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAdapter : ItemAdapter
{
    [Tooltip("Currently Inside Clip")]
    [HideInInspector] public int clipCount = 0;

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
