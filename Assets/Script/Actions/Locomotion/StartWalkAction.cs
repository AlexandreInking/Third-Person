using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWalkAction : LocomotionAction
{
    public StartWalkAction(IController playerMovementController)
        : base(playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).walking = true;
    }
}