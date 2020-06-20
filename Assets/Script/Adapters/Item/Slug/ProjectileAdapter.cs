using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAdapter : UnGrabbableAdapter
{
    #region Impact

    public delegate void Impact(Collision collision);

    public event Impact OnImpact;

    public void TriggerImpact(Collision collision)
    {
        OnImpact?.Invoke(collision);
    }

    #endregion

    [SerializeField] protected Rigidbody rBody;
    [Tooltip("Time Projectile will Destroy After")]
    [SerializeField] float lifeTime;

    protected bool collided = false;

    public override void Initialize()
    {
        Destroy(gameObject, lifeTime);

        OnImpact += (collision =>
        {
            Collided(collision);
        });
    }

    public virtual void Collided(Collision collision)
    {
        collided = true;
    }

    private void Update()
    {
        if (!collided)
        {
            transform.rotation = Quaternion.LookRotation(rBody.velocity);
        }
    }
}
