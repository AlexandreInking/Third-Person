public class PlayerControllerSet : ControllerSet
{
    private void Awake()
    {
        InitializeController<PlayerLocomotionController>();
    }
}
