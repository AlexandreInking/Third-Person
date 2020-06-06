using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Look at CrossHair
public class LookAtAction : Action
{
    Animator animator;
    CombatController combatController;
    CombatProfile combatProfile;

    public override void OnAction()
    {
        //Called on actor's OnAnimatorIK
        animator.SetLookAtPosition(combatController.aimPosition);

        animator.SetLookAtWeight(
            combatProfile.lookWeight,
            combatProfile.bodyWeight,
            combatProfile.headWeight,
            1, combatProfile.clampWeight);
    }

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();
        combatController = actionPack.actionController as CombatController;
        combatProfile = (actor.profile as CombatantProfile).combatProfile;
    }
}
