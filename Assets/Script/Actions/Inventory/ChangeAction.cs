using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Detect Slot Change Input then Change Equipped/UnEquipped Items
/// </summary>
public class ChangeAction : Action
{
    PlayerInventoryController inventoryController;

    PlayerInventory inventory;

    public override void OnAction()
    {
        int slotPointer = 0;

        GameConstants.PlayerSlots.ForEach(slot =>
        {
            if (Input.GetButtonDown(slot))
                inventory.ChangeItem(slotPointer);

            slotPointer++;
        });
    }

    public override void OnInitialize()
    {
        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventoryController = actionPack.actionController as PlayerInventoryController;

    }
}
