public class StartSprintAction : Action
{
    LocomotionController locomotionController;

    public override void OnInitialize()
    {
        locomotionController = actionPack.actionController as LocomotionController;
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);

        locomotionController.sprinting = true;
    }
}