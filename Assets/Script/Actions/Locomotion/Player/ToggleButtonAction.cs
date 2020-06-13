using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonAction : Action
{
    LocomotionController locomotionController;

    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        locomotionController = actionPack.actionController as LocomotionController;

        movementProfile = (actor.profile as PlayerProfile).movementProfile;
    }

    public override void OnAction()
    {
        //Detect for Crouching and Trigger Events
        Toggle<CrouchAction, StandAction>(locomotionController.crouching, movementProfile.crouchToggle, GameConstants.Crouch);
        
        //Detect for Sprinting and Trigger Events
        Toggle<StartSprintAction, StopSprintAction>(locomotionController.sprinting, movementProfile.sprintToggle, GameConstants.Sprint);
        
        //Detect for Walking and Trigger Events
        Toggle<StartWalkAction, StopWalkAction>(locomotionController.walking, movementProfile.walkToggle, GameConstants.Walk);
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
                    actionPack.TakeAction<CompletedAction>();
            }

            else
            {
                if (Input.GetButtonUp(button))
                    actionPack.TakeAction<CompletedAction>();
            }
        }

        else
        {
            if (Input.GetButtonDown(button))
                actionPack.TakeAction<InitiatedAction>();
        }
    }
}