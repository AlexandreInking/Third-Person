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

        CombatController combatController = actor.controllerPack.GetController<CombatController>();

        AnimationController animationController = actor.controllerPack.GetController<AnimationController>();

        CombatActionPack actionPack = combatController.actionPack as CombatActionPack;

        inventory.OnQuickSlotEquipped += (entry => 
        {
            if (!isEquipped)
            {
                return;
            }

            actionPack.GetAction<ReloadAction>().Reload();
        });

        actor.OnAnimationEvent += (eventTag => 
        {
            if (!isEquipped)
            {
                return;
            }

            switch (eventTag)
            {
                case GameConstants.AE_Nock:

                    Attach();

                    break;

                case GameConstants.AE_Fire:

                    Dettach();

                    if (combatController.aiming)
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
            if (!isEquipped)
            {
                return;
            }

            RangedWeapon bow = inventory.ActiveEntry.Value.Item as RangedWeapon;

            RangedWeaponAdapter adapter = inventory.ActiveEntry.Value.Adapter as RangedWeaponAdapter;

            if (adapter.clipCount > 0)
            {
                //Dettach Arrow ; Put Arrow Back in Quiver

                inventory.LoadMagazine(bow.slug, adapter.clipCount);

                adapter.clipCount = 0;
            }

            actionPack.TakeAction<ReloadAction>();
        });

        actionPack.AddActionInitiatedListener<AimFreeAction>(action => 
        {
            if (!isEquipped)
            {
                return;
            }

            Dettach();

            actionPack.GetAction<ReloadAction>().Reload();
        });

        animationController.OnStateInitialized += (state =>
        {
            if (!isEquipped)
            {
                return;
            }

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
