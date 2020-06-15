using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RangedWeaponFlavor), menuName = "SO/Flavors/Weapons/Ranged")]
public class RangedWeaponFlavor : WeaponFlavor
{
    [Space]
    [Header("Aim | Shoot/Throw")]
    public AnimationClip Aim;
    public AnimationClip Shoot;

    [Space]
    [Header("Crosshair")]
    public Sprite TargetSprite;
    public int TargetSize;
}
