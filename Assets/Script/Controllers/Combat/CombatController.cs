using UnityEngine;

public abstract class CombatController : Controller
{
    [HideInInspector] public bool aiming = false;
    [HideInInspector] public Vector3 aimPosition = Vector3.zero;
}
