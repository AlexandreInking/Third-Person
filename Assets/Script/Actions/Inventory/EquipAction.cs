using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAction : Action
{
    PlayerInventory inventory;

    Animator animator;

    int equipHash;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        equipHash = Animator.StringToHash(GameConstants.Equip);

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        #region On Equip

        inventory.OnQuickSlotEquipped += (item =>
        {
            animator.SetTrigger(equipHash);
        });

        #endregion

    }

    public override void OnAction()
    {

    }
}
