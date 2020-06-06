using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAction : Action
{
    PlayerInventoryController inventoryController;

    PlayerInventory inventory;

    Animator animator;

    int equipHash;

    public override void OnAction()
    {

    }

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        equipHash = Animator.StringToHash(GameConstants.Equip);

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventoryController = actionPack.actionController as PlayerInventoryController;

        #region On Equip

        inventory.OnQuickSlotEquipped += (item =>
        {
            animator.SetTrigger(equipHash);
        });

        #endregion

    }
}
