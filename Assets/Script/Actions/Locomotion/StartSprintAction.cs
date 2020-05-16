using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSprintAction : LocomotionAction
{
    public StartSprintAction(IController playerMovementController)
        : base(playerMovementController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {

    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).sprinting = true;
    }
}