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

                    RangedAdapter adapter = inventory.ActiveEntry.Value.Adapter as RangedAdapter;

                    adapter.Reload();

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

        if (adapter.clipCount + adapter.chamberCount >= weapon.liveBarrel.clipSize + weapon.liveBarrel.chamberSize)
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
}
