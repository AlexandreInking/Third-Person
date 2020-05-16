using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownAction : LocomotionAction
{
    public AimDownAction(Animator animator, IController playerMovementController)
        : base(animator: animator, playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).aiming = true;
        _animator.SetTrigger(StaticVariables.AimDown);
    }
}