public class StopSprintAction : LocomotionAction
{
    LocomotionController locomotionController;

    public StopSprintAction(IController locomotionController)
        : base(locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.sprinting = false;
    }
}