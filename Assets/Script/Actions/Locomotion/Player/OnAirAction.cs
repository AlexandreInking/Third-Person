using UnityEngine;

//Detect If Airborne and Trigger Actions
public class OnAirAction : Action
{
    LocomotionController locomotionController;

    CharacterController characterController;

    public override void OnInitialize()
    {
        locomotionController = actionPack.actionController as LocomotionController;

        characterController = actor.GetComponent<CharacterController>();
    }

    public override void OnAction()
    {
        if (locomotionController.airborne)
        {
            if (characterController.isGrounded)
                actionPack.TakeAction<GroundedAction>();
        }

        else
        {
            if (!characterController.isGrounded)
                actionPack.TakeAction<AirborneAction>();
        }

        TriggerActionCompleted(this);
    }
}