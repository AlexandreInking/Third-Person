using UnityEngine;

public class GetRotationAction : Action
{
    PlayerLocomotionController playerMovementController;
    Transform characterTransform;
    Animator animator;
    MovementProfile movementProfile;
    Transform mainCamera;
    Vector2 inputVector;

    public override void OnInitialize()
    {
        mainCamera = Camera.main.transform;

        playerMovementController = actionPack.actionController as PlayerLocomotionController;
        characterTransform = actor.GetComponent<Transform>();
        animator = actor.GetComponent<Animator>();

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

            if (animator.GetCurrentAnimatorStateInfo(GameConstants.BaseLayer).shortNameHash.Equals(GameConstants.locomotionShortHash))
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                    Quaternion.LookRotation(cameraDirection.normalized),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);

            //This Should Be Moved to a Combat Action (or not?)
            else if (animator.GetCurrentAnimatorStateInfo(GameConstants.RangedLayer).shortNameHash.Equals(GameConstants.aimingShortHash))
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

            if (animator.GetCurrentAnimatorStateInfo(GameConstants.BaseLayer).shortNameHash.Equals(GameConstants.locomotionShortHash))
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                    Quaternion.LookRotation((right.normalized * inputVector.x) + (forward.normalized * inputVector.y)),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }
    }
}