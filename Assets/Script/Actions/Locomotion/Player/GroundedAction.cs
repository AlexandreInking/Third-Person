public class GroundedAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;

    public GroundedAction(IController playerMovementController)
        : base(locomotionController: playerMovementController)
    {

    }

    public override void OnInitialize()
    {
        playerMovementController = _locomotionController as PlayerLocomotionController;
    }

    public override void OnAction()
    {
        playerMovementController.airborne = false;
    }
}