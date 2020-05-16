using System;
using UnityEngine;
using UnityEngine.Events;


//TODO: Replace With Actions
class PlayerMovementController : Controller, IController
{
    Camera mainCamera;
    Animator animator;
    CharacterController playerController;
    [HideInInspector] public Vector2 inputVector = Vector2.zero;
    [HideInInspector] public Vector2 rawInputVector = Vector2.zero;

    #region AnimatorDampTime

    [Header("Animator Damp Durations")]

    [Range(0, 1)]
    [SerializeField]
    float speedDampTime = 0.15f;
    [Range(0, 1)]
    [SerializeField]
    float directionDampTime = 0.15f;
    [Range(0f, 1f)]
    [SerializeField]
    float sprintDampTime = 0.15f;
    [Range(0f, 1f)]
    [SerializeField]
    float floorAngleDampTime = 0.15f;

    #endregion

    MovementProfile movementProfile;

    [HideInInspector] public float angle = 0f;
    [HideInInspector] public float direction = 0f;
    [HideInInspector] public float floorAngle = 0f;
    [HideInInspector] public bool aiming = false;
    [HideInInspector] public bool crouching = false;
    [HideInInspector] public bool sprinting = false;
    [HideInInspector] public bool walking = false;
    [HideInInspector] public bool airborne = false;
    [HideInInspector] public Vector3 fallVelocity = Vector3.zero;
    Vector3 cameraDirection = Vector3.zero;
    Transform characterTransform;
    
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

    private void Awake()
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
    }

    private void Start()
    {
        InitializeActionSet<PlayerMovementActionSet>();

        mainCamera = Camera.main;

        Character controlledCharacter = controllerSet.controlledCharacter;

        animator = controlledCharacter.GetComponent<Animator>();
        playerController = controlledCharacter.GetComponent<CharacterController>();
        characterTransform = controlledCharacter.transform;

        PlayerProfile playerProfile = controlledCharacter.profile as PlayerProfile;
        movementProfile = playerProfile.movementProfile;
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxis(StaticVariables.Horizontal), Input.GetAxis(StaticVariables.Vertical));
        rawInputVector = new Vector2(Input.GetAxisRaw(StaticVariables.Horizontal), Input.GetAxisRaw(StaticVariables.Vertical));

        //Detect for Aiming and Trigger Events
        Toggle<AimDownAction, AimFreeAction>(aiming, movementProfile.aimToggle, StaticVariables.Fire2);
        //Detect for Crouching and Trigger Events
        Toggle<CrouchAction, StandAction>(crouching, movementProfile.crouchToggle, StaticVariables.Crouch);
        //Detect for Sprinting and Trigger Events
        Toggle<StartSprintAction, StopSprintAction>(sprinting, movementProfile.sprintToggle, StaticVariables.Sprint);
        //Detect for Walking and Trigger Events
        Toggle<StartWalkAction, StopWalkAction>(walking, movementProfile.walkToggle, StaticVariables.Walk);
        //Detect If Airborne and Trigger Events
        Airborne();
        //Calculates Angle, Direction and Rotation and Rotates Player
        CalculateAngleDirectionRotation();
        //Calculates fallVelocity.y
        CalculateFallDisplacement();
        //Calculates floorAngle : Angle between floor and Player then Apply Down Force On Slope
        ApplyDownSlopeForce();
        //Sets Player Animator's Movement Parameters
        SetAnimatorParameters();
    }

    void Toggle<InitiatedAction, CompletedAction>(bool on, bool toggle, string button) 
        where InitiatedAction : Action
        where CompletedAction : Action
    {
        if (on)
        {
            if (toggle)
            {
                if (Input.GetButtonDown(button))
                    actionSet.TakeAction<CompletedAction>();
            }

            else
            {
                if (Input.GetButtonUp(button))
                    actionSet.TakeAction<CompletedAction>();
            }
        }

        else
        {
            if (Input.GetButtonDown(button))
                actionSet.TakeAction<InitiatedAction>();
        }
    }

    void Airborne()
    {
        if (airborne)
        {
            if (playerController.isGrounded)
                actionSet.TakeAction<GroundedAction>();
        }

        else
        {
            if (!playerController.isGrounded)
                actionSet.TakeAction<AirborneAction>();
        }
    }

    void SetAnimatorParameters()
    {
        animator.SetFloat(directionHash, direction, directionDampTime, Time.deltaTime);
        animator.SetFloat(angleHash, angle, speedDampTime, Time.deltaTime);
        animator.SetFloat(fallDistanceHash, fallVelocity.y, 0f, Time.deltaTime);
        animator.SetFloat(floorAngleHash, floorAngle, floorAngleDampTime, Time.deltaTime);

        animator.SetFloat(speedHash,
            inputVector.normalized.magnitude *
            (walking ? 0.5f : 1f),
            speedDampTime, Time.deltaTime);

        animator.SetFloat(rawSpeedHash,
            rawInputVector.normalized.magnitude);

        animator.SetBool(isGroundedHash, playerController.isGrounded);
        animator.SetBool(aimingHash, aiming);
        animator.SetBool(crouchingHash, crouching);

        if (aiming)
        {
            animator.SetFloat(rightHash, inputVector.normalized.x, speedDampTime, Time.deltaTime);
            animator.SetFloat(forwardHash, inputVector.normalized.y, speedDampTime, Time.deltaTime);
        }

        animator.SetFloat(sprintFactorHash,
            sprinting ? 1 : 0.5f, sprintDampTime,
            Time.deltaTime);

        animator.SetFloat(groundedFootHash,
            playerController.isGrounded ?
            animator.GetFloat(leftFootUpHash) - animator.GetFloat(rightFootUpHash) : animator.GetFloat(groundedFootHash));
    }

    void CalculateAngleDirection()
    {
        Vector3 stickDirection = aiming ? Vector3.forward : new Vector3(inputVector.x, 0, inputVector.y);

        //Camera Rotation
        cameraDirection = mainCamera.transform.forward;

        cameraDirection.y = 0f;
        cameraDirection.Normalize();

        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        //Input to Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, characterTransform.forward);

        float angleRootToMove = Vector3.Angle(characterTransform.forward, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        angle = angleRootToMove;

        angleRootToMove /= 180;

        direction = angleRootToMove * movementProfile.directionSpeed;
    }

    void CalculateAngleDirectionRotation()
    {
        CalculateAngleDirection();

        float rotationSpeedMultiplier = 1f;

        if (aiming)
        {
            rotationSpeedMultiplier += 2f;

            if (animator.GetCurrentAnimatorStateInfo(StaticVariables.BaseLayer).shortNameHash.Equals(locomotionShortHash))
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                    Quaternion.LookRotation(cameraDirection),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }

        else
        {
            if (crouching)
                rotationSpeedMultiplier += 2f;

            Vector3 forward = mainCamera.transform.forward;
            Vector3 right = mainCamera.transform.right;
            forward.y = 0;
            right.y = 0;

            if (animator.GetCurrentAnimatorStateInfo(StaticVariables.BaseLayer).shortNameHash.Equals(locomotionShortHash))
                characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation,
                    Quaternion.LookRotation((right.normalized * inputVector.x) + (forward.normalized * inputVector.y)),
                    movementProfile.rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }
    }

    void CalculateFloorAngle()
    {
        RaycastHit hit;

        if (Physics.Raycast(characterTransform.position, Vector3.down, out hit, movementProfile.slopeDetectionDistance))
            floorAngle = Vector3.Angle(hit.normal, characterTransform.up);
        else
            floorAngle = 0f;
    }

    void CalculateFallDisplacement()
    {
        if (playerController.isGrounded)
            fallVelocity.y = 0f;

        else
            fallVelocity.y += Physics.gravity.y * Time.deltaTime;
    }

    void ApplyDownSlopeForce()
    {
        CalculateFloorAngle();

        if (floorAngle > 0 && !inputVector.Equals(Vector2.zero))
            playerController.Move(Vector3.down * floorAngle * movementProfile.onSlopeExtraDownForce * Time.deltaTime);
    }
}
