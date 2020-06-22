using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Item
{
    [Space]
    [Header("Life Time")]
    [Tooltip("Time After Slug and Effects will Disappear")]
    [Range(0f, 10f)]
    public float lifeTime = 3f;
}
