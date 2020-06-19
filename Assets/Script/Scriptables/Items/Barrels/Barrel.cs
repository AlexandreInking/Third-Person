using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Barrel : Item
{
    [Space]
    [Tooltip("Currently Barrel Chamber Size")]
    public int chamberSize;

    [Space]
    [Tooltip("Currently Loaded Cartridge")]
    public Slug liveSlug;

    [Space]
    [Header("Rounds")]
    [Tooltip("Amount of Rounds A Single Clip Can Hold")]
    public int clipSize;

    [Space]
    [Header("Fire Rate")]
    [Range(0f, 10f)]
    [Tooltip("time before another shot can be fired")]
    public float fireRate = 1f;
}
