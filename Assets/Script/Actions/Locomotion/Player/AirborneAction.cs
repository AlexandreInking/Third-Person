using UnityEngine;

public class AirborneAction : Action
{
    Transform mainCamera;

    PlayerLocomotionController playerMovementController;

    CharacterController characterController;

    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        mainCamera = Camera.main.transform;

        playerMovementController = actionPack.actionController as PlayerLocomotionController;

        characterController = actor.GetComponent<CharacterController>();

        movementProfile = (actor.profile as MotileProfile).movementProfile;
    }

    public override void OnAction()
    {
        Vector3 inputVector = playerMovementController.inputVector;

        playerMovementController.airborne = true;

        Vector3 forward = mainCamera.forward;

        Vector3 right = mainCamera.right;

        forward.y = 0;

        right.y = 0;

        characterController.Move((right.normalized * inputVector.x) 
            + (forward.normalized * inputVector.y) *
            (playerMovementController.sprinting ? 1f : 0.5f) * movementProfile.airbornePushForce * Time.deltaTime);
    }
}