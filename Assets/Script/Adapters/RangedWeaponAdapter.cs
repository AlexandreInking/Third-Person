using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Move clip count to Adapter
public class RangedWeaponAdapter : ItemAdapter
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
