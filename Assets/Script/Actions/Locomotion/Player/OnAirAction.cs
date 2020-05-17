using UnityEngine;

//Detect If Airborne and Trigger Actions
public class OnAirAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;

    public OnAirAction(IController playerMovementController, CharacterController playerController)
        : base(locomotionController: playerMovementController, characterController: playerController)
    {

    }

    public override void OnInitialize()
    {
        playerMovementController = _locomotionController as PlayerLocomotionController;
    }

    public override void OnAction()
    {
        if (playerMovementController.airborne)
        {
            if (_characterController.isGrounded)
                actionSet.TakeAction<GroundedAction>();
        }

        else
        {
            if (!_characterController.isGrounded)
                actionSet.TakeAction<AirborneAction>();
        }

        TriggerActionCompleted(this);
    }
}