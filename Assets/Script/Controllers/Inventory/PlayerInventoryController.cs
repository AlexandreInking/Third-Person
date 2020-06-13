using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : InventoryController
{
    public PlayerInventory inventory;

    public override void OnInitialize()
    {
        inventory = (controlledCharacter.profile as CombatantProfile).inventory as PlayerInventory;

        InitializeActionPack<InventoryActionPack>();
    }

    private void Start()
    {
        inventory.OnItemCloned += (adapter =>
        {
            adapter.Actor = controlledCharacter;

            if (adapter is ItemAdapter)
            {
                (adapter as ItemAdapter).UnEquip();
            }
        });

        //Test Bow
        inventory.HoldQuickSlot(inventory.GetInventoryEntry(2), 2);
    }

    private void Update()
    {
        actionPack.TakeAction<ChangeAction>();
    }
}
