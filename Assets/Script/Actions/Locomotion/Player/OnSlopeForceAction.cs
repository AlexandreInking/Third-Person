using UnityEngine;

/// <summary>
/// Applies down force when on slope
/// </summary>
public class OnSlopeForceAction : Action
{
    PlayerLocomotionController playerMovementController;

    CharacterController characterController;

    Transform characterTransform;

    Vector2 inputVector;

    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        playerMovementController = actionPack.actionController as PlayerLocomotionController;

        characterTransform = actor.GetComponent<Transform>();

        characterController = actor.GetComponent<CharacterController>();

        movementProfile = (actor.profile as PlayerProfile).movementProfile;
    }

    public override void OnAction()
    {
        inputVector = playerMovementController.inputVector;

        CalculateFloorAngle();

        ApplyOnSlopeDownForce();
    }

    void CalculateFloorAngle()
    {
        RaycastHit hit;

        if (Physics.Raycast(characterTransform.position, Vector3.down, out hit, movementProfile.slopeDetectionDistance))
            playerMovementController.floorAngle = Vector3.Angle(hit.normal, characterTransform.up);
        else
            playerMovementController.floorAngle = 0f;
    }

    void ApplyOnSlopeDownForce()
    {
        if (playerMovementController.floorAngle > 0 && !inputVector.Equals(Vector2.zero))
            characterController.Move(Vector3.down * playerMovementController.floorAngle * movementProfile.onSlopeExtraDownForce * Time.deltaTime);
    }
}