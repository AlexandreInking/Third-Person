using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryController : InventoryController
{
    public PlayerInventory inventory;

    public override void OnInitialize()
    {
        inventory = (controlledCharacter.profile as CombatantProfile).inventory as PlayerInventory;

        inventory.OnQuickSlotEquipped += (entry =>
        {
            Equipped = inventory.ActiveEntry.Value.Item;
        });

        inventory.OnQuickSlotUnEquipped += (entry =>
        {
            Equipped = null;
        });

        InitializeActionPack<InventoryActionPack>();
    }

    private void Start()
    {
        inventory.OnItemCloned += (adapter =>
        {
            adapter.Actor = controlledCharacter;

            if (adapter is GrabbableAdapter)
            {
                (adapter as GrabbableAdapter).UnEquip();
            }
        });

        //Test Bow
        inventory.HoldQuickSlot(inventory.GetInventoryEntry(
            inventory.Library.FirstOrDefault(pair => pair.Value == inventory.testBow).Key.index
            ), 2);
    }

    private void Update()
    {
        actionPack.TakeAction<ChangeAction>();
    }
}
