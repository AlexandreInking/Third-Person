using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementActionSet : ActionSet
{
    Animator animator;
    Transform playerTransform;
    CharacterController playerController;
    PlayerMovementController playerMovementController;
    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();
        playerTransform = actor.GetComponent<Transform>();
        playerController = actor.GetComponent<CharacterController>();
        playerMovementController = actionController as PlayerMovementController;

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
    }
}
