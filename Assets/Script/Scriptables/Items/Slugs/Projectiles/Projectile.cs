using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Slug
{
    [Space]
    [Tooltip("Range of Projectile")]
    public float range;
    [Tooltip("Speed of Projectile")]
    public float speed;
}