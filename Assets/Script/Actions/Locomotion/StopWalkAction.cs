public class StopWalkAction : LocomotionAction
{
    LocomotionController locomotionController;

    public StopWalkAction(IController locomotionController)
        : base(locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.walking = false;
    }
}