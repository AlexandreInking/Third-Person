using UnityEngine;

public abstract class LocomotionController : Controller
{
    [HideInInspector] public float angle = 0f;
    [HideInInspector] public float direction = 0f;
    [HideInInspector] public float floorAngle = 0f;
    [HideInInspector] public bool strafing = false;
    [HideInInspector] public bool crouching = false;
    [HideInInspector] public bool sprinting = false;
    [HideInInspector] public bool walking = false;
    [HideInInspector] public bool airborne = false;
    [HideInInspector] public Vector3 fallVelocity = Vector3.zero;
}
