using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneAction : LocomotionAction
{
    Transform mainCamera;
    PlayerMovementController playerMovementController;
    bool sprinting;
    Vector2 inputVector;
    MovementProfile movementProfile;

    public AirborneAction(IController playerMovementController, CharacterController playerController)
        : base(playerMovementController: playerMovementController, playerController : playerController)
    {
    }

    public override void OnInitialize()
    {
        mainCamera = Camera.main.transform;
        playerMovementController = _playerMovementController as PlayerMovementController;
        sprinting = playerMovementController.sprinting;
        inputVector = playerMovementController.inputVector;

        movementProfile = (actor.profile as PlayerProfile).movementProfile;
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);

        playerMovementController.airborne = true;

        Vector3 forward = mainCamera.forward;
        Vector3 right = mainCamera.right;
        forward.y = 0;
        right.y = 0;

        _playerController.Move((right.normalized * inputVector.x) 
            + (forward.normalized * inputVector.y) *
            (sprinting ? 1f : 0.5f) * movementProfile.airbornePushForce * Time.deltaTime);
    }
}