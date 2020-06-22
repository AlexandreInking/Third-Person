using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    [Space]
    [Tooltip("Loaded Arrow not to Shoot")]
    public GameObject dummyPrefab;

    [Space]
    [Header("Arrow Draw Speed")]
    [Tooltip("Default (Initial) Speed Of Arrow")]
    [Range(0f, 5f)]
    public float defaultSpeed;
    [Tooltip("Controls By How Much Arrow Speed is Multiplied Proportional to Pull Back")]
    public float speedFator = 2f;
    [Tooltip("Speed Limit")]
    public float speedThreshold = 2f;


    private void OnEnable()
    {
        speed = defaultSpeed;
    }
}