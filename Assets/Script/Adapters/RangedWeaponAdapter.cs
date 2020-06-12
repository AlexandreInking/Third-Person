using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Move clip count to Adapter
public class RangedWeaponAdapter : ItemAdapter
{
    public override void Equip()
    {
        base.Equip();

        transform.SetParent((inventory as PlayerInventory).leftHand);
    }

    public override void UnEquip()
    {
        base.UnEquip();

        transform.SetParent((inventory as PlayerInventory).rangedWeapon);
    }
}
