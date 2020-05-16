using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFreeAction : LocomotionAction
{
    public AimFreeAction(Animator animator, IController playerMovementController)
        : base(animator: animator, playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).aiming = false;
        _animator.SetTrigger(StaticVariables.AimFree);
    }
}