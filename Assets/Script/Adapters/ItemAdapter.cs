using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAdapter : Adapter
{
    [Header("Equip | UnEquip")]
    [SerializeField] AnimationClip EquipClip;
    [SerializeField] AnimationClip UnEquipClip;

    [Space]
    [Header("Animation")]
    [SerializeField] new Animation animation;

    protected PlayerInventory inventory;

    protected bool isEquipped = false;

    public override void Initialize()
    {
        //Add Clips to Animation
        animation.AddClip(EquipClip, EquipClip.name);
        animation.AddClip(UnEquipClip, UnEquipClip.name);

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventory.OnQuickSlotEquipped += (entry =>
        {
            if (entry.Value.Adapter == this)
            {
                isEquipped = true;

                return;
            }

            isEquipped = false;
        });

        inventory.OnQuickSlotUnEquipped += (entry =>
        {
            isEquipped = false;
        });
    }

    public virtual void Equip()
    {
        animation.Play(EquipClip.name);
    }

    public virtual void UnEquip()
    {
        animation.Play(UnEquipClip.name);
    }
}
