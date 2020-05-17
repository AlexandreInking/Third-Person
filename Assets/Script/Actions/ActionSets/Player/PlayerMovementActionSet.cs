using UnityEngine;
public class PlayerMovementActionSet : ActionSet
{
    Animator animator;
    Transform playerTransform;
    CharacterController playerController;
    PlayerLocomotionController playerMovementController;
    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();
        playerTransform = actor.GetComponent<Transform>();
        playerController = actor.GetComponent<CharacterController>();
        playerMovementController = actionController as PlayerLocomotionController;

        InitializeAction<CrouchAction>(animator, playerMovementController);
        InitializeAction<StandAction>(animator, playerMovementController);
        InitializeAction<AimFreeAction>(animator, playerMovementController);
        InitializeAction<AimDownAction>(animator, playerMovementController);
        InitializeAction<StartSprintAction>(playerMovementController);
        InitializeAction<StopSprintAction>(playerMovementController);
        InitializeAction<StartWalkAction>(playerMovementController);
        InitializeAction<StopWalkAction>(playerMovementController);
        InitializeAction<AirborneAction>(playerMovementController, playerController);
        InitializeAction<GroundedAction>(playerMovementController);
        InitializeAction<SetAnimatorAction>(animator, playerMovementController);
        InitializeAction<GetAngleAction>(playerMovementController, playerTransform);
        InitializeAction<GetDirectionAction>(playerMovementController);
        InitializeAction<GetRotationAction>(animator, playerMovementController, playerTransform);
        InitializeAction<ToggleButtonAction>(playerMovementController);
        InitializeAction<OnSlopeForceAction>(playerMovementController, playerTransform, playerController);
        InitializeAction<OnAirAction>(playerMovementController, playerController);
        InitializeAction<GetDropAction>(playerMovementController);
    }
}
