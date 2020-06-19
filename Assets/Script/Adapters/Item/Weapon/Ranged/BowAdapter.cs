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

    GameObject dummyArrowInstance;

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

    bool slugChange = false;

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

            const string AE_Draw = "Draw";

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

                case AE_Draw:

                    if (!isEquipped)
                    {
                        return;
                    }

                    Arrow arrow = (inventory.ActiveEntry.Value.Item as Bow).liveSlug as Arrow;

                    if (slugChange)
                    {
                        slugChange = false;

                        if (combatController.aiming)
                        {
                            dummyArrowInstance = Instantiate(arrow.dummyPrefab, inventory.rightHand);
                        }
                    }

                    else
                    {
                        dummyArrowInstance = Instantiate(arrow.dummyPrefab, inventory.rightHand);
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

                inventory.LoadMagazine(bow.liveSlug, clipCount);

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

        actionPack.AddActionInitiatedListener<ReloadAction>(action => 
        {
            if (!isEquipped)
            {
                return;
            }

            Dettach();
        });

        actionPack.AddActionInitiatedListener<SlugChangeAction>(action => 
        {
            if (!isEquipped)
            {
                return;
            }

            slugChange = true;

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

                ((inventory.ActiveEntry.Value.Item as Bow).liveSlug as Projectile).speed =  (speedMultiplier / speedFator);
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

        ((inventory.ActiveEntry.Value.Item as Bow).liveSlug as Projectile).speed = 1;

        if (dummyArrowInstance != null)
        {
            Destroy(dummyArrowInstance);
        }
    }
}
