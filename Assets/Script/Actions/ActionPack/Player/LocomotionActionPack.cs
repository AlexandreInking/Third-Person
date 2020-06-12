using UnityEngine;

public class LocomotionActionPack : ActionPack
{
    //TODO: Get Drop Action For Strafing
    public override void OnInitialize()
    {
        #region Crouch | Stand

        InitializeAction<CrouchAction>();

        InitializeAction<StandAction>();

        #endregion

        #region STRAFE

        InitializeAction<FreeAction>();

        InitializeAction<StrafeAction>();

        #endregion

        #region SPRINT

        InitializeAction<StartSprintAction>();

        InitializeAction<StopSprintAction>();

        #endregion

        #region WALK

        InitializeAction<StartWalkAction>();

        InitializeAction<StopWalkAction>();

        #endregion

        #region AIRBORNE

        InitializeAction<AirborneAction>();

        InitializeAction<GroundedAction>();

        InitializeAction<OnAirAction>();

        InitializeAction<GetDropAction>();

        #endregion

        #region Animator

        InitializeAction<SetAnimatorAction>();

        #endregion

        #region Calculation

        InitializeAction<GetAngleAction>();

        InitializeAction<GetDirectionAction>();

        InitializeAction<GetRotationAction>();

        InitializeAction<OnSlopeForceAction>();

        #endregion

        InitializeAction<ToggleButtonAction>();
    }
}
