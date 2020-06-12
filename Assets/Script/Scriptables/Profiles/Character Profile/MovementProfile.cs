using UnityEngine;

[CreateAssetMenu(fileName = "Movement Profile", menuName = "SO/Profile/Character/Movement")]
public class MovementProfile : ScriptableObject
{
    [Header("Direction and Rotation")]

    [SerializeField]
    public float directionSpeed = 3f;
    [SerializeField]
    public float rotationSpeed = 20f;

    [Header("Slope and Airborne")]

    [Range(0f, 1f)]
    [SerializeField]
    public float slopeDetectionDistance = 0.1f;
    [Range(0f, 100f)]
    [SerializeField]
    public float onSlopeExtraDownForce = 5f;
    [SerializeField]
    public float airbornePushForce = 5f;

    [Header("Toggles")]

    [SerializeField]
    public bool crouchToggle;
    [SerializeField]
    public bool sprintToggle;
    [SerializeField]
    public bool walkToggle;

}
