using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAdapter : RangedAdapter
{
    [Space]
    [Header("Bow String")]
    [Tooltip("Bow String Attach | Dettach")]
    [SerializeField] Transform nock;

    Vector3 rootNockPosition;

    AnimationController animationController;

    #region Arrow Speed

    float speedMultiplier = 1f;

    float startTime = float.MinValue;

    bool aimed = false;

    [Space]
    [Tooltip("Controls By How Much Arrow Speed is Multiplied Proportional to Pull Back")]
    [SerializeField] float speedFator = 2f;
    [SerializeField] float speedThreshold = 3.5f;

    #endregion

    public override void Initialize()
    {
        base.Initialize();

        rootNockPosition = nock.localPosition;

        CombatController combatController = actor.controllerPack.GetController<CombatController>();

        animationController = actor.controllerPack.GetController<AnimationController>();

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

                case GameConstants.AE_Aimed:

                    startTime = Time.time;

                    speedMultiplier = 1f;

                    aimed = true;

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

            Bow bow = inventory.ActiveEntry.Value.Item as Bow;

            if (clipCount > 0)
            {
                //Dettach Arrow ; Put Arrow Back in Quiver

                inventory.LoadMagazine(bow.slug, clipCount);

                clipCount = 0;
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

    private void Update()
    {
        if (isEquipped)
        {
            if (aimed && animationController.state[GameConstants.RangedLayer] == GameConstants.AS_Aiming)
            {
                speedMultiplier += (Time.time - startTime) * Time.deltaTime;

                if (speedMultiplier > speedThreshold)
                {
                    aimed = false;
                }

                ((inventory.ActiveEntry.Value.Item as Bow).slug as Projectile).speed =  (speedMultiplier / speedFator);
            }
        }
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

        //Arrow Speed
        aimed = false;

        ((inventory.ActiveEntry.Value.Item as Bow).slug as Projectile).speed = 1;
    }
}
