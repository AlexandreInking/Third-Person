using UnityEngine;

public class GameConstants
{
    #region Controls_Buttons

    public static readonly string Vertical = "Vertical";
    public static readonly string Horizontal = "Horizontal";
    public static readonly string Fire2 = "Fire2";
    public static readonly string Sprint = "Sprint";
    public static readonly string Walk = "Walk";
    public static readonly string Crouch = "Crouch";

    #endregion

    #region AnimatorParameters

    public static readonly int BaseLayer = 0;

    public static readonly string RawSpeed = "RawSpeed";
    public static readonly string Direction = "Direction";
    public static readonly string Angle = "Angle";
    public static readonly string LeftFootUp = "LeftFootUp";
    public static readonly string RightFootUp = "RightFootUp";
    public static readonly string GroundedFoot = "GroundedFoot";
    public static readonly string FreeStateShortPath = "Free";

    public static readonly int aimDownHah = Animator.StringToHash("AimDown");
    public static readonly int aimFreeHash = Animator.StringToHash("AimFree");
    public static readonly int crouchDownHash = Animator.StringToHash("CrouchDown");
    public static readonly int crouchFreeHash = Animator.StringToHash("CrouchFree");
    public static readonly int locomotionShortHash = Animator.StringToHash("Locomotion");
    public static readonly int turnBlendShortHash = Animator.StringToHash("Turn Blend");
    public static readonly int sprintFactorHash = Animator.StringToHash("SprintFactor");
    public static readonly int speedHash = Animator.StringToHash("Speed");
    public static readonly int floorAngleHash = Animator.StringToHash("FloorAngle");
    public static readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
    public static readonly int aimingHash = Animator.StringToHash("Aiming");
    public static readonly int crouchingHash = Animator.StringToHash("Crouching");
    public static readonly int fallDistanceHash = Animator.StringToHash("FallDistance");
    public static readonly int rightHash = Animator.StringToHash("Right");
    public static readonly int forwardHash = Animator.StringToHash("Forward");

    #endregion
}
