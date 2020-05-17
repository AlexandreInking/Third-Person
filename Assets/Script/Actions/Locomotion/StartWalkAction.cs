public class StartWalkAction : LocomotionAction
{
    LocomotionController locomotionController;

    public StartWalkAction(IController locomotionController)
        : base(locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.walking = true;
    }
}