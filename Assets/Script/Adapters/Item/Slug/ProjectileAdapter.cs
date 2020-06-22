using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAdapter : SlugAdapter
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

    float lifeTime;

    protected bool collided = false;

    public override void Initialize()
    {
        lifeTime = (((actor.profile as CombatantProfile).inventory as PlayerInventory)
            .ActiveEntry.Value.Item as RangedWeapon)
            .liveBarrel.liveSlug.lifeTime;

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
