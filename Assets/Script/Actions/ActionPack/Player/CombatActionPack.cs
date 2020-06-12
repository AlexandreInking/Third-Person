using UnityEngine;

public class CombatActionPack : ActionPack
{
    public override void OnInitialize()
    {
        //oREDER oF iNITIALIZATION Matters for referencing

        #region Ranged Combat

        #region AIMING

        InitializeAction<AimDownAction>();

        InitializeAction<AimFreeAction>();

        InitializeAction<UpdateAimAction>();

        InitializeAction<DrawTargetAction>();

        InitializeAction<LookAtAction>();

        InitializeAction<ToggleAimAction>();

        #endregion

        InitializeAction<ReloadAction>();

        #endregion

        InitializeAction<AttackAction>();
    }
}
