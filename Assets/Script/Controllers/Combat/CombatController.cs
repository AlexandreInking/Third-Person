using UnityEngine;

public abstract class CombatController : Controller
{
    [HideInInspector] public Vector3 aimPosition = Vector3.zero;

    [HideInInspector] public bool aiming = false;
}
