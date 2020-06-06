using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnEquipAction : Action
{
    PlayerInventoryController inventoryController;

    PlayerInventory inventory;

    Animator animator;

    int unEquipHash;

    public override void OnAction()
    {

    }

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        unEquipHash = Animator.StringToHash(GameConstants.UnEquip);

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventoryController = actionPack.actionController as PlayerInventoryController;

        #region On UnEquip

        inventory.OnQuickSlotUnEquipped += (item =>
        {
            animator.SetTrigger(unEquipHash);
        });

        #endregion
    }
}
