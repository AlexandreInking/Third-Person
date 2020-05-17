using UnityEngine;

//Gets fall Displacement/Velocity
public class GetDropAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;

    public GetDropAction(IController playerMovementController)
        : base(locomotionController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {
        playerMovementController = _locomotionController as PlayerLocomotionController;
    }

    public override void OnAction()
    {
        if (!playerMovementController.airborne)
            playerMovementController.fallVelocity.y = 0f;

        else
            playerMovementController.fallVelocity.y += Physics.gravity.y * Time.deltaTime;
    }
}