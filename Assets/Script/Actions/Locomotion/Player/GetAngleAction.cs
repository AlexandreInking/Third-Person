using UnityEngine;

public class GetAngleAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;
    Vector3 stickDirection;
    Vector2 inputVector;
    Transform mainCamera;
    MovementProfile movementProfile;

    public GetAngleAction(IController playerMovementController, Transform playerTransform)
        : base(locomotionController: playerMovementController, characterTransform : playerTransform)
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
        stickDirection = playerMovementController.aiming ? Vector3.forward : new Vector3(inputVector.x, 0, inputVector.y);

        CalculateAngle();

        TriggerActionCompleted(this);
    }

    void CalculateAngle()
    {
        //Camera Rotation
        Vector3 cameraDirection = mainCamera.forward;
        cameraDirection.y = 0f;

        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection.normalized);

        //Input to Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, _characterTranform.forward);

        playerMovementController.angle = Vector3.Angle(_characterTranform.forward, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
    }
}