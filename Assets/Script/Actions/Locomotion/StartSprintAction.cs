public class StartSprintAction : LocomotionAction
{
    LocomotionController locomotionController;

    public StartSprintAction(IController locomotionController)
        : base(locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.sprinting = true;
    }
}