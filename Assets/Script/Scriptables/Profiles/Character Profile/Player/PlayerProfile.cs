using UnityEngine;

[CreateAssetMenu(fileName =nameof(PlayerProfile), menuName = "Profile/Character/Player")]
public class PlayerProfile : CharacterProfile
{
    public MovementProfile movementProfile;

    #region AnimatorDampTime

    [Header("Animator Damp Durations")]

    [Range(0, 1)]
    public float speedDampTime = 0.15f;
    [Range(0, 1)]
    public float directionDampTime = 0.15f;
    [Range(0f, 1f)]
    public float sprintDampTime = 0.15f;
    [Range(0f, 1f)]
    public float floorAngleDampTime = 0.15f;

    #endregion
}
