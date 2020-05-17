using UnityEngine;

public class GetRotationAction : LocomotionAction
{
    PlayerLocomotionController playerMovementController;
    MovementProfile movementProfile;
    Transform mainCamera;
    Vector2 inputVector;

    public GetRotationAction(Animator animator, IController playerMovementController, Transform playerTransform)
        : base(animator : animator, locomotionController : playerMovementController, characterTransform : playerTransform)
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

        CalculateRotation();
    }

    void CalculateRotation()
    {
        Vector3 cameraDirection = mainCamera.forward;
        cameraDirection.y = 0f;

        float rotationSpeedMultiplier = 1f;

        if (playerMovementController.aiming)
        {
            rotationSpeedMultiplier += 2f;

            if (_animator.GetCurrentAnimatorStateInfo(GameConstants.BaseLayer).shortNameHash.Equals(GameConstants.locomotionShortHash))
                _characterTranform.rotation = Quaternion.RotateTowards(_characterTranform.rotation,
                    Quaternion.LookRotation(cameraDirection.normalized),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }

        else
        {
            if (playerMovementController.crouching)
                rotationSpeedMultiplier += 2f;

            Vector3 forward = mainCamera.forward;
            Vector3 right = mainCamera.right;
            forward.y = 0;
            right.y = 0;

            if (_animator.GetCurrentAnimatorStateInfo(GameConstants.BaseLayer).shortNameHash.Equals(GameConstants.locomotionShortHash))
                _characterTranform.rotation = Quaternion.RotateTowards(_characterTranform.rotation,
                    Quaternion.LookRotation((right.normalized * inputVector.x) + (forward.normalized * inputVector.y)),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }
    }
}