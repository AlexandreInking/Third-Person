using System;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAction : Action
{
    Animator animator;

    PlayerInventory inventory;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        actor.OnAnimationEvent += (eventTag => 
        {
            switch (eventTag)
            {
                case GameConstants.AE_Reload:

                    Reload();

                    TriggerActionCompleted(this);

                    break;

                default:

                    break;
            }
        });
    }

    public override void OnAction()
    {
        RangedWeapon weapon = inventory.ActiveEntry.Value.Item as RangedWeapon;

        RangedAdapter adapter = inventory.ActiveEntry.Value.Adapter as RangedAdapter;

        if (adapter.clipCount == weapon.liveBarrel.clipSize)
        {
            //Full Clip
            Debug.LogError("Full Clip");

            return;
        }

        if (inventory.GetMagazine(weapon.liveBarrel.liveSlug).count <= 0)
        {
            //eMPTY mAG
            Debug.LogError("Empty Magazine");

            return;
        }

        TriggerActionInitiated(this);

        animator.SetTrigger(GameConstants.reloadHash);
    }

    public void Reload()
    {
        RangedWeapon weapon = inventory.ActiveEntry.Value.Item as RangedWeapon;

        RangedAdapter adapter = inventory.ActiveEntry.Value.Adapter as RangedAdapter;

        //Remaining Clip Slots
        int open = weapon.liveBarrel.clipSize - adapter.clipCount;

        if (inventory.GetMagazine(weapon.liveBarrel.liveSlug).count > open)
        {
            adapter.clipCount = weapon.liveBarrel.clipSize;

            inventory.UnLoadMagazine(weapon.liveBarrel.liveSlug, open);
        }

        else
        {
            adapter.clipCount += inventory.GetMagazine(weapon.liveBarrel.liveSlug).count;

            inventory.ClearMagazine(weapon.liveBarrel.liveSlug);
        }
    }
}
