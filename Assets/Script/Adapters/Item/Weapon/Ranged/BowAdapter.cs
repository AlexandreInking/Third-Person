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

    GameObject dummyArrowInstance;

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

        animationController = actor.controllerPack.GetController<AnimationController>();

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

                default:
                    break;
            }
        });

        actor.OnActionInitiated += (action => 
        {
            if (!isEquipped)
            {
                return;
            }

            if 
            (
            action is AimFreeAction
            ||
            action is ReloadAction
            )

            {
                Dettach();
            }

            if (action is AimDownAction)
            {
                ClearChamber();

                actor.controllerPack.GetController<CombatController>().actionPack.TakeAction<ReloadAction>();
            }
        });

        actor.OnActionCompleted += (action => 
        {
            if (!isEquipped)
            {
                return;
            }

            if (action is AttackAction)
            {
                CombatController controller = actor.controllerPack.GetController<CombatController>();

                if (controller.aiming)
                {
                    controller.actionPack.TakeAction<ReloadAction>();
                }

                else
                {
                    Reload();
                }
            }
        });

        animationController.OnStateComplete += (state => 
        {
            if (!isEquipped)
            {
                return;
            }

            switch (state)
            {
                case GameConstants.AS_Aiming:

                    //Arrow Speed
                    aimed = false;

                    ((inventory.ActiveEntry.Value.Item as Bow).liveBarrel.liveSlug as Projectile).speed = 1;

                    break;

                default:

                    break;
            }
        });

        animationController.OnStateInitialized += (state => 
        {
            if (!isEquipped)
            {
                return;
            }

            Dettach();

            switch (state)
            {
                case GameConstants.AS_Idle:

                    Reload();

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
            #region Draw Speed Increase

            if (aimed && animationController.state[GameConstants.RangedLayer] == GameConstants.AS_Aiming)
            {
                speedMultiplier += (Time.time - startTime) * Time.deltaTime;

                if (speedMultiplier > speedThreshold)
                {
                    aimed = false;
                }

                ((inventory.ActiveEntry.Value.Item as Bow).liveBarrel.liveSlug as Projectile).speed = (speedMultiplier / speedFator);
            }

            #endregion

            #region OverLoad

            if (Input.GetKeyDown(KeyCode.E))
            {
                InstantOverLoad();
            }

            #endregion
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

        //Destroy Dummy Arrow Instance
        Destroy(dummyArrowInstance);
    }

    protected override void Fire()
    {
        Dettach();
    }

    protected override void Draw()
    {
        Arrow arrow = (inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel.liveSlug as Arrow;

        dummyArrowInstance = Instantiate(arrow.dummyPrefab, inventory.rightHand);
    }
}
