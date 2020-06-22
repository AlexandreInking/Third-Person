public class PlayerControllerPack : ControllerPack
{
    public override void OnInitialize()
    {
        //oREDER oF iNITIALIZATION Matters for referencing
        InitializeController<AnimationController>();

        InitializeController<PlayerLocomotionController>();

        InitializeController<PlayerCombatController>();

        InitializeController<PlayerInventoryController>();

        InitializeController<CameraController>();
    }
}
