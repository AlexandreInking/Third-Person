using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorAction : LocomotionAction
{
    #region AnimatorHashes

    int leftFootUpHash;
    int rightFootUpHash;
    int directionHash;
    int angleHash;
    int speedHash;
    int rawSpeedHash;
    int sprintFactorHash;
    int groundedFootHash;
    int floorAngleHash;
    int isGroundedHash;
    int aimingHash;
    int crouchingHash;
    int fallDistanceHash;
    int aimDownHah;
    int aimFreeHash;
    int rightHash;
    int forwardHash;
    int turnBlendShortHash;
    int locomotionShortHash;

    #endregion

    public Vector2 inputVector;
    public Vector2 rawInputVector;
    public float angle;
    public float direction;
    public float floorAngle;
    public bool aiming;
    public bool crouching;
    public bool sprinting;
    public bool walking;
    public bool airborne;
    public Vector3 fallVelocity;

    PlayerMovementController playerMovementController;

    public SetAnimatorAction(Animator animator, IController playerMovementController)
        : base(animator: animator, playerMovementController: playerMovementController)
    {
    }

    public override void OnInitialize()
    {
        #region SetAnimatorHashes

        leftFootUpHash = Animator.StringToHash(StaticVariables.RightFootUp);
        rightFootUpHash = Animator.StringToHash(StaticVariables.LeftFootUp);
        directionHash = Animator.StringToHash(StaticVariables.Direction);
        angleHash = Animator.StringToHash(StaticVariables.Angle);
        speedHash = Animator.StringToHash(StaticVariables.Speed);
        rawSpeedHash = Animator.StringToHash(StaticVariables.RawSpeed);
        sprintFactorHash = Animator.StringToHash(StaticVariables.SprintFactor);
        groundedFootHash = Animator.StringToHash(StaticVariables.GroundedFoot);
        floorAngleHash = Animator.StringToHash(StaticVariables.FloorAngle);
        isGroundedHash = Animator.StringToHash(StaticVariables.IsGrounded);
        aimingHash = Animator.StringToHash(StaticVariables.Aiming);
        crouchingHash = Animator.StringToHash(StaticVariables.Crouching);
        fallDistanceHash = Animator.StringToHash(StaticVariables.FallDistance);
        aimDownHah = Animator.StringToHash(StaticVariables.AimDown);
        aimFreeHash = Animator.StringToHash(StaticVariables.AimFree);
        rightHash = Animator.StringToHash(StaticVariables.Right);
        forwardHash = Animator.StringToHash(StaticVariables.Forward);
        turnBlendShortHash = Animator.StringToHash(StaticVariables.TurnBlendShortPath);
        locomotionShortHash = Animator.StringToHash(StaticVariables.LocomotionShortPath);

        #endregion

        playerMovementController = _playerMovementController as PlayerMovementController;

        angle = playerMovementController.angle;
        direction = playerMovementController.direction;
        floorAngle = playerMovementController.floorAngle;
        aiming = playerMovementController.aiming;
        crouching = playerMovementController.crouching;
        sprinting = playerMovementController.sprinting;
        walking = playerMovementController.walking;
        airborne = playerMovementController.airborne;
        fallVelocity = playerMovementController.fallVelocity;
    }

    public override void OnAction()
    {
        inputVector = new Vector2(Input.GetAxis(StaticVariables.Horizontal), Input.GetAxis(StaticVariables.Vertical));
        rawInputVector = new Vector2(Input.GetAxisRaw(StaticVariables.Horizontal), Input.GetAxisRaw(StaticVariables.Vertical));

        SetAnimatorParameters();
    }

    void SetAnimatorParameters()
    {
        _animator.SetFloat(directionHash, direction, directionDampTime, Time.deltaTime);
        _animator.SetFloat(angleHash, angle, speedDampTime, Time.deltaTime);
        _animator.SetFloat(fallDistanceHash, fallVelocity.y, 0f, Time.deltaTime);
        _animator.SetFloat(floorAngleHash, floorAngle, floorAngleDampTime, Time.deltaTime);

        _animator.SetFloat(speedHash, 
            inputVector.normalized.magnitude *
            (walking ? 0.5f : 1f),
            speedDampTime, Time.deltaTime);

        _animator.SetFloat(rawSpeedHash,
            rawInputVector.normalized.magnitude);

        _animator.SetBool(isGroundedHash, !airborne);
        _animator.SetBool(aimingHash, aiming);
        _animator.SetBool(crouchingHash, crouching);

        if (aiming)
        {
            _animator.SetFloat(rightHash, inputVector.normalized.x, speedDampTime, Time.deltaTime);
            _animator.SetFloat(forwardHash, inputVector.normalized.y, speedDampTime, Time.deltaTime);
        }

        _animator.SetFloat(sprintFactorHash, 
            sprinting ? 1 : 0.5f, sprintDampTime,
            Time.deltaTime);

        _animator.SetFloat(groundedFootHash,
            !airborne ?
            _animator.GetFloat(leftFootUpHash) - _animator.GetFloat(rightFootUpHash) : _animator.GetFloat(groundedFootHash));
    }
}