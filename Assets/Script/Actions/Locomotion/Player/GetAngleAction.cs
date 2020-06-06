using UnityEngine;

public class GetAngleAction : Action
{
    PlayerLocomotionController playerMovementController;
    Vector3 stickDirection;
    Vector2 inputVector;
    Transform mainCamera;
    Transform characterTransform;
    MovementProfile movementProfile;

    public override void OnInitialize()
    {
        mainCamera = Camera.main.transform;
        playerMovementController = actionPack.actionController as PlayerLocomotionController;
        characterTransform = actor.GetComponent<Transform>();
        movementProfile = (actor.profile as PlayerProfile).movementProfile;
    }

    public override void OnAction()
    {
        inputVector = playerMovementController.inputVector;
        stickDirection = playerMovementController.strafing ? Vector3.forward : new Vector3(inputVector.x, 0, inputVector.y);

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
        Vector3 axisSign = Vector3.Cross(moveDirection, characterTransform.forward);

        playerMovementController.angle = Vector3.Angle(characterTransform.forward, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
    }
}