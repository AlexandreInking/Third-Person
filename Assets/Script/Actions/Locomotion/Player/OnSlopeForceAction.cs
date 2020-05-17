using UnityEngine;

//Applies down force when on slope
public class OnSlopeForceAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;
    Vector2 inputVector;
    MovementProfile movementProfile;

    public OnSlopeForceAction(IController playerMovementController, Transform playerTransform, CharacterController playerController)
        : base(locomotionController: playerMovementController, characterTransform : playerTransform, characterController: playerController)
    {

    }

    public override void OnInitialize()
    {
        playerMovementController = _locomotionController as PlayerLocomotionController;
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

        if (Physics.Raycast(_characterTranform.position, Vector3.down, out hit, movementProfile.slopeDetectionDistance))
            playerMovementController.floorAngle = Vector3.Angle(hit.normal, _characterTranform.up);
        else
            playerMovementController.floorAngle = 0f;
    }

    void ApplyOnSlopeDownForce()
    {
        if (playerMovementController.floorAngle > 0 && !inputVector.Equals(Vector2.zero))
            _characterController.Move(Vector3.down * playerMovementController.floorAngle * movementProfile.onSlopeExtraDownForce * Time.deltaTime);
    }
}