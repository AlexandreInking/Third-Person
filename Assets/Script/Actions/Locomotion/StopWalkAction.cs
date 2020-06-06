public class StopWalkAction : Action
{
    LocomotionController locomotionController;

    public override void OnInitialize()
    {
        locomotionController = actionPack.actionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.walking = false;
    }
}