using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    #region Input

    public static readonly string Vertical = "Vertical";
    public static readonly string Horizontal = "Horizontal";
    public static readonly string Fire1 = "Fire1";
    public static readonly string Fire2 = "Fire2";
    public static readonly string Sprint = "Sprint";
    public static readonly string Walk = "Walk";
    public static readonly string Crouch = "Crouch";

    public static readonly List<string> PlayerSlots = new List<string>
    {
        "Slot0",
        "Slot1",
        "Slot2",
        "Slot3",
        "Slot4"
    };

    #endregion

    #region Animator

    #region Layer

    public static readonly int BaseLayer = 0;
    public static readonly int RangedLayer = 1;

    #endregion

    #region Event

    public const string AE_Equip = "Equip";
    public const string AE_UnEquip = "UnEquip";
    public const string AE_Nock = "Nock";
    public const string AE_Fire = "Fire";

    #endregion

    #region Parameter Hash

    public static readonly string RawSpeed = "RawSpeed";
    public static readonly string AimDown = "AimDown";
    public static readonly string AimFree = "AimFree";
    public static readonly string Fire = "Fire";
    public static readonly string Direction = "Direction";
    public static readonly string Angle = "Angle";
    public static readonly string LeftFootUp = "LeftFootUp";
    public static readonly string RightFootUp = "RightFootUp";
    public static readonly string GroundedFoot = "GroundedFoot";
    public static readonly string FreeStateShortPath = "Free";
    public const string Equip = "Equip";
    public const string UnEquip = "UnEquip";

    public static readonly int strafeHah = Animator.StringToHash("Strafe");
    public static readonly int freeHash = Animator.StringToHash("Free");
    public static readonly int crouchDownHash = Animator.StringToHash("CrouchDown");
    public static readonly int crouchFreeHash = Animator.StringToHash("CrouchFree");
    public static readonly int locomotionShortHash = Animator.StringToHash("Locomotion");
    public static readonly int turnBlendShortHash = Animator.StringToHash("Turn Blend");
    public static readonly int aimingShortHash = Animator.StringToHash("Aiming");
    public static readonly int sprintFactorHash = Animator.StringToHash("SprintFactor");
    public static readonly int speedHash = Animator.StringToHash("Speed");
    public static readonly int floorAngleHash = Animator.StringToHash("FloorAngle");
    public static readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
    public static readonly int strafingHash = Animator.StringToHash("Strafing");
    public static readonly int crouchingHash = Animator.StringToHash("Crouching");
    public static readonly int fallDistanceHash = Animator.StringToHash("FallDistance");
    public static readonly int rightHash = Animator.StringToHash("Right");
    public static readonly int forwardHash = Animator.StringToHash("Forward");

    #endregion

    #endregion
}