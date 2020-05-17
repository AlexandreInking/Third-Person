public class GetDirectionAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;
    MovementProfile movementProfile;

    public GetDirectionAction(IController playerMovementController)
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
        float angleRootToMove = playerMovementController.angle;

        angleRootToMove /= 180;

        playerMovementController.direction = angleRootToMove * movementProfile.directionSpeed;
    }
}