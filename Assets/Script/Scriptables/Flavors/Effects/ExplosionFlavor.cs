using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ExplosionFlavor), menuName = "SO/Flavors/Effects/Explosion")]
public class ExplosionFlavor : Flavor
{
    [Space]
    [Tooltip("Explosion On Impact ; Particle System")]
    [SerializeField] GameObject ExplosionPrefab;
    [Space]
    [Header("Explosion : Radius | Force")]
    [Range(0f, 10f)]
    [SerializeField] float radius;
    [Range(0f, 100f)]
    [SerializeField] float force;


    public void Explode(Transform explodeAt)
    {
        ParticleSystem explosion = Instantiate(ExplosionPrefab, explodeAt).GetComponent<ParticleSystem>();

        explosion.Play();


        //Explosion
        Collider[] colliders = Physics.OverlapSphere(explodeAt.position, radius);

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
            rBody.AddExplosionForce(force, explodeAt.position, radius, 1f, ForceMode.Impulse);
        }
    }
}
