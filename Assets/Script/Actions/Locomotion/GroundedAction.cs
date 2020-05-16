using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedAction : LocomotionAction
{
    public GroundedAction(IController playerMovementController)
        : base(playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).airborne = false;
    }
}