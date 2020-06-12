using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Space]
    [Header("Fire Rate")]
    [Range(0f, 10f)]
    [Tooltip("time before another shot can be fired")]
    public float fireRate = 1f;

    [Space]
    [Header("Rounds")]
    public int clipSize = 1;
    [Tooltip("Currently Inside Clip")]
    public int clipCount = 1;
    public int magazine = 50;
    [Space]
    [Tooltip("Kind of Ammo it takes")]
    public Slug cartridge;
}
