using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdapterAction : Action
{
    PlayerInventory inventory;

    public override void OnInitialize()
    {
        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        //Add Listeners
        actor.OnAnimationEvent += (eventTag =>
        {
            switch (eventTag)
            {
                case GameConstants.AE_Equip:
                    (inventory.ActiveEntry.Value.Adapter as ItemAdapter).Equip();
                    break;
                case GameConstants.AE_UnEquip:
                    (inventory.ActiveEntry.Value.Adapter as ItemAdapter).UnEquip();
                    break;
                default:
                    break;
            }
        });
    }

    public override void OnAction()
    {
    }
}
