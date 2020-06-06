using UnityEngine;

/// <summary>
/// Sets Player Animator's Movement Parameters
/// </summary>
public class SetAnimatorAction : Action
{
    #region AnimatorHashes

    int leftFootUpHash;
    int rightFootUpHash;
    int directionHash;
    int angleHash;
    int rawSpeedHash;
    int groundedFootHash;

    #endregion

    public Vector2 inputVector;
    public Vector2 rawInputVector;

    PlayerLocomotionController locomotionController;
    Animator animator;

    PlayerProfile playerProfile;

    public override void OnInitialize()
    {
        #region SetAnimatorHashes

        leftFootUpHash = Animator.StringToHash(GameConstants.RightFootUp);
        rightFootUpHash = Animator.StringToHash(GameConstants.LeftFootUp);
        directionHash = Animator.StringToHash(GameConstants.Direction);
        angleHash = Animator.StringToHash(GameConstants.Angle);
        rawSpeedHash = Animator.StringToHash(GameConstants.RawSpeed);
        groundedFootHash = Animator.StringToHash(GameConstants.GroundedFoot);

        #endregion

        locomotionController = actionPack.actionController as PlayerLocomotionController;
        animator = actor.GetComponent<Animator>();

        playerProfile = actor.profile as PlayerProfile;
    }

    public override void OnAction()
    {
        inputVector = locomotionController.inputVector;
        rawInputVector = locomotionController.rawInputVector;

        SetAnimatorParameters();

        TriggerActionCompleted(this);
    }

    void SetAnimatorParameters()
    {
        animator.SetFloat(directionHash, locomotionController.direction, playerProfile.directionDampTime, Time.deltaTime);
        animator.SetFloat(angleHash, locomotionController.angle, playerProfile.speedDampTime, Time.deltaTime);
        animator.SetFloat(GameConstants.fallDistanceHash, locomotionController.fallVelocity.y, 0f, Time.deltaTime);
        animator.SetFloat(GameConstants.floorAngleHash, locomotionController.floorAngle, playerProfile.floorAngleDampTime, Time.deltaTime);

        animator.SetFloat(GameConstants.speedHash, 
            inputVector.normalized.magnitude *
            (locomotionController.walking ? 0.5f : 1f),
            playerProfile.speedDampTime, Time.deltaTime);

        animator.SetFloat(rawSpeedHash,
            rawInputVector.normalized.magnitude);

        animator.SetBool(GameConstants.isGroundedHash, !locomotionController.airborne);
        animator.SetBool(GameConstants.strafingHash, locomotionController.strafing);
        animator.SetBool(GameConstants.crouchingHash, locomotionController.crouching);

        if (locomotionController.strafing)
        {
            animator.SetFloat(GameConstants.rightHash, inputVector.normalized.x, playerProfile.speedDampTime, Time.deltaTime);
            animator.SetFloat(GameConstants.forwardHash, inputVector.normalized.y, playerProfile.speedDampTime, Time.deltaTime);
        }

        animator.SetFloat(GameConstants.sprintFactorHash,
            locomotionController.sprinting ? 1 : 0.5f, playerProfile.sprintDampTime,
            Time.deltaTime);

        animator.SetFloat(groundedFootHash,
            !locomotionController.airborne ?
            animator.GetFloat(leftFootUpHash) - animator.GetFloat(rightFootUpHash) : animator.GetFloat(groundedFootHash));
    }
}