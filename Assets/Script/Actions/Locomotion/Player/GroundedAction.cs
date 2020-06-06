public class GroundedAction : Action
{
    LocomotionController playerMovementController;

    public override void OnInitialize()
    {
        playerMovementController = actionPack.actionController as LocomotionController;
    }

    public override void OnAction()
    {
        playerMovementController.airborne = false;
    }
}