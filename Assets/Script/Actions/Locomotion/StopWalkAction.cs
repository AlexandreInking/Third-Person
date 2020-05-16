using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWalkAction : LocomotionAction
{
    public StopWalkAction(IController playerMovementController)
        : base(playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).walking = false;
    }
}