using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =nameof(PlayerProfile), menuName = "Profile/Character/Player")]
public class PlayerProfile : CharacterProfile
{
    public MovementProfile movementProfile;

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
}
