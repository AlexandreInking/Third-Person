using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;
    MovementProfile movementProfile;

    public ToggleButtonAction(IController playerMovementController)
        : base(locomotionController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {
        playerMovementController = _locomotionController as PlayerLocomotionController;
        movementProfile = (actor.profile as PlayerProfile).movementProfile;
    }

    public override void OnAction()
    {
        //Detect for Aiming and Trigger Events
        Toggle<AimDownAction, AimFreeAction>(playerMovementController.aiming, movementProfile.aimToggle, GameConstants.Fire2);
        //Detect for Crouching and Trigger Events
        Toggle<CrouchAction, StandAction>(playerMovementController.crouching, movementProfile.crouchToggle, GameConstants.Crouch);
        //Detect for Sprinting and Trigger Events
        Toggle<StartSprintAction, StopSprintAction>(playerMovementController.sprinting, movementProfile.sprintToggle, GameConstants.Sprint);
        //Detect for Walking and Trigger Events
        Toggle<StartWalkAction, StopWalkAction>(playerMovementController.walking, movementProfile.walkToggle, GameConstants.Walk);
    }

    void Toggle<InitiatedAction, CompletedAction>(bool on, bool toggle, string button)
        where InitiatedAction : Action
        where CompletedAction : Action
    {
        if (on)
        {
            if (toggle)
            {
                if (Input.GetButtonDown(button))
                    actionSet.TakeAction<CompletedAction>();
            }

            else
            {
                if (Input.GetButtonUp(button))
                    actionSet.TakeAction<CompletedAction>();
            }
        }

        else
        {
            if (Input.GetButtonDown(button))
                actionSet.TakeAction<InitiatedAction>();
        }
    }
}