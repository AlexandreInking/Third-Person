using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CombatProfile), menuName = "SO/Profile/Character/Combat")]
public class CombatProfile : ScriptableObject
{
    #region Ranged

    [Space]
    [Header("Aiming")]
    [Range(10f, 100f)]
    public float aimDistance = 30f;
    [Range(-1f, 1f)]
    public float aimOffsetX = 0f;
    [Range(-1f, 1f)]
    public float aimOffsetY = 0f;
    [Tooltip("All Layers except the once you want to ignore...Player | Friendly ")]
    public LayerMask aimLayers;

    [Space]
    [Header("Look Weights")]
    [Range(0, 1f)]
    public float lookWeight = 1f;
    [Range(0, 1f)]
    public float bodyWeight = 1f;
    [Range(0, 1f)]
    public float headWeight = 1f;
    [Range(0, 1f)]
    public float clampWeight = 1f;

    [Space]
    [Header("Toggles")]
    public bool aimToggle;

    #endregion
}
