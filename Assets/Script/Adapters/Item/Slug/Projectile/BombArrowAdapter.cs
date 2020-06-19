using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrowAdapter : ArrowAdapter
{
    [Space]
    [Tooltip("Arrow Explosion On Impact")]
    [SerializeField] GameObject ExplosionPrefab;
    [Space]
    [Header("Explosion : Radius | Force")]
    [Range(0f, 10f)]
    [SerializeField] float radius;
    [Range(0f, 100f)]
    [SerializeField] float force;

    public override void Collided(Collision collision)
    {
        base.Collided(collision);

        ParticleSystem explosion = Instantiate(ExplosionPrefab, transform).GetComponent<ParticleSystem>();

        explosion.Play();


        //Explosion
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        var rBodies = new List<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            if (collider.attachedRigidbody != null && !rBodies.Contains(collider.attachedRigidbody))
            {
                rBodies.Add(collider.attachedRigidbody);
            }
        }
        foreach (var rBody in rBodies)
        {
            rBody.AddExplosionForce(force, transform.position, radius, 1, ForceMode.Impulse);
        }
    }
}
