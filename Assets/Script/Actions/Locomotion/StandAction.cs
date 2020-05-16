using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandAction : LocomotionAction
{
    public StandAction(Animator animator, IController playerMovementController)
        : base(animator: animator, playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).crouching = false;
        _animator.SetTrigger(StaticVariables.CrouchFree);
    }
}