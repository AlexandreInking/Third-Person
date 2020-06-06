public class GetDirectionAction : Action
{
    PlayerLocomotionController playerMovementController;
    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        playerMovementController = actionPack.actionController as PlayerLocomotionController;
        movementProfile = (actor.profile as MotileProfile).movementProfile;
    }

    public override void OnAction()
    {
        float angleRootToMove = playerMovementController.angle;

        angleRootToMove /= 180;

        playerMovementController.direction = angleRootToMove * movementProfile.directionSpeed;
    }
}