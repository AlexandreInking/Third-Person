using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownAction : Action
{
    CombatController combatController;
    Animator animator;
    int aimDownHash;

    public override void OnInitialize()
    {
        combatController = actionPack.actionController as CombatController;
        animator = actor.GetComponent<Animator>();
        aimDownHash = Animator.StringToHash(GameConstants.AimDown);
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        combatController.aiming = true;
        animator.SetTrigger(aimDownHash);
    }
}
