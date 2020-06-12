using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Look at CrossHair
public class LookAtAction : Action
{
    Animator animator;

    CombatController combatController;

    CombatProfile combatProfile;

    PlayerInventory inventory;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        combatController = actionPack.actionController as CombatController;

        combatProfile = (actor.profile as CombatantProfile).combatProfile;

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventory.OnQuickSlotEquipped += (entry =>
        {
            if (entry.Value is RangedWeapon)
            {
                actor.OnAnimatorIk += Look;
            }
        });

        inventory.OnQuickSlotUnEquipped += (entry =>
        {
            if (entry.Value is RangedWeapon)
            {
                actor.OnAnimatorIk -= Look;
            }
        });
    }

    public override void OnAction()
    {
        
    }

    void Look()
    {
        animator.SetLookAtPosition(combatController.aimPosition);

        animator.SetLookAtWeight(
            combatProfile.lookWeight,
            combatProfile.bodyWeight,
            combatProfile.headWeight,
            //Eye Weight
            1, combatProfile.clampWeight);
    }
}
