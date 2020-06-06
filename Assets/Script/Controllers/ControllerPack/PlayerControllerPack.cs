public class PlayerControllerPack : ControllerPack
{
    public override void OnInitialize()
    {
        InitializeController<PlayerLocomotionController>();
        InitializeController<PlayerCombatController>();
        InitializeController<PlayerInventoryController>();
    }
}
