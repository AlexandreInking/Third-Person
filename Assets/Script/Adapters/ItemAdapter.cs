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

    protected Inventory inventory;

    public override void Initialize()
    {
        //Add Clips to Animation
        animation.AddClip(EquipClip, EquipClip.name);
        animation.AddClip(UnEquipClip, UnEquipClip.name);

        inventory = (actor.profile as CombatantProfile).inventory;

        //Add Listeners
        actor.OnAnimationEvent += (eventTag => 
        {
            switch (eventTag)
            {
                case GameConstants.AE_Equip:
                    Equip();
                    break;
                case GameConstants.AE_UnEquip:
                    UnEquip();
                    break;
                default:
                    break;
            }
        });

        //Default State
        UnEquip();
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
