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

        Animator animator = actor.GetComponent<Animator>();

        CombatController combatController = actor.controllerPack.GetController<CombatController>();

        AnimationController animationController = actor.controllerPack.GetController<AnimationController>();

        CombatActionPack actionPack = combatController.actionPack as CombatActionPack;

        (inventory as PlayerInventory).OnQuickSlotEquipped += (entry => 
        {
            actionPack.GetAction<ReloadAction>().Reload();
        });

        actor.OnAnimationEvent += (eventTag => 
        {
            switch (eventTag)
            {
                case GameConstants.AE_Nock:

                    Attach();

                    break;

                case GameConstants.AE_Fire:

                    Dettach();

                    if (animator.GetBool(GameConstants.aimingHash))
                    {
                        actionPack.TakeAction<ReloadAction>();
                    }

                    else
                    {
                        actionPack.GetAction<ReloadAction>().Reload();
                    }

                    break;

                default:
                    break;
            }
        });

        actionPack.AddActionInitiatedListener<AimDownAction>(action => 
        {
            RangedWeapon bow = (inventory as PlayerInventory).ActiveEntry.Value as RangedWeapon;

            if (bow.clipCount > 0)
            {
                //Dettach Arrow ; Put Arrow Back in Quiver
                bow.magazine += bow.clipCount;
                bow.clipCount = 0;
            }

            actionPack.TakeAction<ReloadAction>();
        });

        actionPack.AddActionInitiatedListener<AimFreeAction>(action => 
        {
            Dettach();

            actionPack.GetAction<ReloadAction>().Reload();
        });

        animationController.OnStateInitialized += (state =>
        {
            switch (state)
            {
                case GameConstants.AS_Idle:
                    Dettach();
                    break;
                default:
                    break;
            }
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
