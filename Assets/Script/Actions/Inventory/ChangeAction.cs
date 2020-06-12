using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Detect Slot Change Input then Change Equipped/UnEquipped Items
/// </summary>
public class ChangeAction : Action
{
    PlayerInventory inventory;

    public override void OnInitialize()
    {
        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;
    }

    public override void OnAction()
    {
        int slotPointer = 0;

        GameConstants.PlayerSlots.ForEach(slot =>
        {
            if (Input.GetButtonDown(slot))
            {
                inventory.ChangeItem(slotPointer);
            }

            slotPointer++;
        });
    }
}
