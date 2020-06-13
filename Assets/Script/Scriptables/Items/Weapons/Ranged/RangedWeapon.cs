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
    [Tooltip("Amount of Rounds A Single Clip Can Hold")]
    public int clipSize = 1;
    [Tooltip("Total Bullets Outside Clip")]
    public int magazine = 50;
    [Space]
    [Tooltip("Kind of Ammo it takes")]
    public Slug cartridge;
}
