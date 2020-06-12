using UnityEngine;

public class GetRotationAction : Action
{
    PlayerLocomotionController playerMovementController;

    Transform characterTransform;

    AnimationController animationController;

    MovementProfile movementProfile;

    Transform mainCamera;

    Vector2 inputVector;

    public override void OnInitialize()
    {
        mainCamera = Camera.main.transform;

        playerMovementController = actionPack.actionController as PlayerLocomotionController;

        characterTransform = actor.GetComponent<Transform>();

        animationController = actor.controllerPack.GetController<AnimationController>();

        movementProfile = (actor.profile as MotileProfile).movementProfile;
    }

    public override void OnAction()
    {
        inputVector = playerMovementController.inputVector;

        CalculateRotation();
    }

    void CalculateRotation()
    {
        Vector3 cameraDirection = mainCamera.forward;
        cameraDirection.y = 0f;

        float rotationSpeedMultiplier = 1f;

        if (playerMovementController.strafing)
        {
            rotationSpeedMultiplier += 4f;

            if (animationController.state[GameConstants.BaseLayer] == GameConstants.AS_Locomotion)
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                    Quaternion.LookRotation(cameraDirection.normalized),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);

            else
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                         Quaternion.LookRotation(cameraDirection.normalized),
                         movementProfile.rotationSpeed * (rotationSpeedMultiplier / 1.5f) * Time.deltaTime);

        }

        else
        {
            if (playerMovementController.crouching)
                rotationSpeedMultiplier += 2f;

            Vector3 forward = mainCamera.forward;
            Vector3 right = mainCamera.right;
            forward.y = 0;
            right.y = 0;

            if (animationController.state[GameConstants.BaseLayer] == GameConstants.AS_Locomotion)
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                    Quaternion.LookRotation((right.normalized * inputVector.x) + (forward.normalized * inputVector.y)),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }
    }
}