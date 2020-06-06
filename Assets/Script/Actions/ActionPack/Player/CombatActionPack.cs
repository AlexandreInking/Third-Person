using UnityEngine;

public class CombatActionPack : ActionPack
{
    public override void OnInitialize()
    {
        InitializeAction<UpdateAimAction>();

        InitializeAction<DrawTargetAction>();

        InitializeAction<LookAtAction>();

        InitializeAction<AttackAction>();

        InitializeAction<AimDownAction>();

        InitializeAction<AimFreeAction>();

        InitializeAction<ToggleAimAction>();
    }
}
