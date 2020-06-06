using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAdapter : RangedWeaponAdapter
{
    [Space]
    [Header("Bow String")]
    [Tooltip("Bow String Attach | Dettach")]
    [SerializeField] Transform nock;

    Vector3 rootNockPosition;

    public override void Initialize()
    {
        base.Initialize();

        rootNockPosition = nock.localPosition;

        actor.OnAnimationEvent += (eventTag => 
        {
            switch (eventTag)
            {
                case GameConstants.AE_Nock:
                    Attach();
                    break;
                case GameConstants.AE_Fire:
                    Dettach();
                    break;
                default:
                    break;
            }
        });


        actor.controllerPack.GetController<CombatController>().actionPack
            .AddActionInitiatedListener<AimFreeAction>(action => 
            {
                Dettach();
            });
    }

    /// <summary>
    /// Attaches Bow String
    /// </summary>
    public void Attach()
    {
        nock.SetParent((inventory as PlayerInventory).rightHand);
        nock.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Detaches Bow String
    /// </summary>
    public void Dettach()
    {
        nock.SetParent(transform);
        nock.localPosition = rootNockPosition;
    }
}
