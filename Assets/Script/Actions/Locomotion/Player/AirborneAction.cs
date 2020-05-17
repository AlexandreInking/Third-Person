using UnityEngine;

public class AirborneAction : LocomotionAction
{
    Transform mainCamera;
    PlayerLocomotionController playerMovementController;
    Vector3 inputVector;

    MovementProfile movementProfile;

    public AirborneAction(IController playerMovementController, CharacterController playerController)
        : base(locomotionController: playerMovementController, characterController : playerController)
    {
    }

    public override void OnInitialize()
    {
        mainCamera = Camera.main.transform;
        playerMovementController = _locomotionController as PlayerLocomotionController;

        movementProfile = (actor.profile as PlayerProfile).movementProfile;
    }

    public override void OnAction()
    {
        inputVector = playerMovementController.inputVector;

        playerMovementController.airborne = true;

        Vector3 forward = mainCamera.forward;
        Vector3 right = mainCamera.right;
        forward.y = 0;
        right.y = 0;

        _characterController.Move((right.normalized * inputVector.x) 
            + (forward.normalized * inputVector.y) *
            (playerMovementController.sprinting ? 1f : 0.5f) * movementProfile.airbornePushForce * Time.deltaTime);
    }
}