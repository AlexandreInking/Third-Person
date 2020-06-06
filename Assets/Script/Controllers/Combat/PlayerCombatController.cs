
using UnityEngine;

public class PlayerCombatController : CombatController
{
    PlayerInventory inventory;

    public override void OnInitialize()
    {
        inventory = (controlledCharacter.profile as CombatantProfile).inventory as PlayerInventory;

        InitializeActionPack<CombatActionPack>();
    }

    private void Start()
    {
        controlledCharacter.OnAnimatorIk += delegate 
        {
            if (aiming)
                actionPack.TakeAction<LookAtAction>();
        };

        controllerPack.GetController<LocomotionController>()
            .actionPack.AddActionInitiatedListener<StartSprintAction>(action =>
            {
                if (aiming)
                    actionPack.TakeAction<AimFreeAction>();
            });

        controllerPack.GetController<PlayerInventoryController>().inventory
            .OnQuickSlotUnEquipped += (entry =>
            {
                if (entry.Value is RangedWeapon)
                    actionPack.TakeAction<AimFreeAction>();
            });
    }

    private void Update()
    {
        if (inventory.ActiveEntry.Key.isEquipped && inventory.ActiveEntry.Value is RangedWeapon)
        {
            actionPack.TakeAction<UpdateAimAction>();
            actionPack.TakeAction<ToggleAimAction>();
        }

        actionPack.TakeAction<AttackAction>();
    }

    private void OnGUI()
    {
        if (inventory.ActiveEntry.Key.isEquipped && inventory.ActiveEntry.Value is RangedWeapon)
            actionPack.TakeAction<DrawTargetAction>();
    }
}
