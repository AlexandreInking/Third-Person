using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFreeAction : Action
{
    CombatController combatController;
    Animator animator;
    int aimFreeHash;

    public override void OnInitialize()
    {
        combatController = actionPack.actionController as CombatController;
        animator = actor.GetComponent<Animator>();
        aimFreeHash = Animator.StringToHash(GameConstants.AimFree);
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        combatController.aiming = false;
        animator.SetTrigger(aimFreeHash);
    }
}
