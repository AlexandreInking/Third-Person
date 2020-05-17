using UnityEngine;

//Sets Player Animator's Movement Parameters
public class SetAnimatorAction : LocomotionAction
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

    PlayerLocomotionController playerMovementController;

    PlayerProfile playerProfile;

    public SetAnimatorAction(Animator animator, IController playerMovementController)
        : base(animator: animator, locomotionController: playerMovementController)
    {
    }

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

        playerMovementController = _locomotionController as PlayerLocomotionController;

        playerProfile = actor.profile as PlayerProfile;
    }

    public override void OnAction()
    {
        inputVector = playerMovementController.inputVector;
        rawInputVector = playerMovementController.rawInputVector;

        SetAnimatorParameters();

        TriggerActionCompleted(this);
    }

    void SetAnimatorParameters()
    {
        _animator.SetFloat(directionHash, playerMovementController.direction, playerProfile.directionDampTime, Time.deltaTime);
        _animator.SetFloat(angleHash, playerMovementController.angle, playerProfile.speedDampTime, Time.deltaTime);
        _animator.SetFloat(GameConstants.fallDistanceHash, playerMovementController.fallVelocity.y, 0f, Time.deltaTime);
        _animator.SetFloat(GameConstants.floorAngleHash, playerMovementController.floorAngle, playerProfile.floorAngleDampTime, Time.deltaTime);

        _animator.SetFloat(GameConstants.speedHash, 
            inputVector.normalized.magnitude *
            (playerMovementController.walking ? 0.5f : 1f),
            playerProfile.speedDampTime, Time.deltaTime);

        _animator.SetFloat(rawSpeedHash,
            rawInputVector.normalized.magnitude);

        _animator.SetBool(GameConstants.isGroundedHash, !playerMovementController.airborne);
        _animator.SetBool(GameConstants.aimingHash, playerMovementController.aiming);
        _animator.SetBool(GameConstants.crouchingHash, playerMovementController.crouching);

        if (playerMovementController.aiming)
        {
            _animator.SetFloat(GameConstants.rightHash, inputVector.normalized.x, playerProfile.speedDampTime, Time.deltaTime);
            _animator.SetFloat(GameConstants.forwardHash, inputVector.normalized.y, playerProfile.speedDampTime, Time.deltaTime);
        }

        _animator.SetFloat(GameConstants.sprintFactorHash,
            playerMovementController.sprinting ? 1 : 0.5f, playerProfile.sprintDampTime,
            Time.deltaTime);

        _animator.SetFloat(groundedFootHash,
            !playerMovementController.airborne ?
            _animator.GetFloat(leftFootUpHash) - _animator.GetFloat(rightFootUpHash) : _animator.GetFloat(groundedFootHash));
    }
}