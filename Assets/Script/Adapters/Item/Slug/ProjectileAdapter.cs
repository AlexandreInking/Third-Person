using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAdapter : UnEqiuppableItemAdapter
{


    #region Impact

    public delegate void Impact();

    public event Impact OnImpact;

    public void TriggerImpact()
    {
        OnImpact?.Invoke();
    }

    #endregion

    [SerializeField] protected Rigidbody rBody;
    [Tooltip("Time Projectile will Destroy After")]
    [SerializeField] float lifeTime;

    protected bool collided = false;

    public override void Initialize()
    {
        Destroy(gameObject, lifeTime);

        OnImpact += delegate 
        {
            Collided();
        };
    }

    public virtual void Collided()
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
