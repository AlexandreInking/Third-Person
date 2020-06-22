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

    [Space]
    [Header("Path Visualization")]
    [SerializeField] LineRenderer path;
    [SerializeField] int pathSegment = 10;

    Vector3 rootNockPosition;

    AnimationController animationController;

    GameObject dummyArrowInstance;

    #region Arrow Speed

    float speedMultiplier = 1f;

    float startTime = float.MinValue;

    bool aimed = false;

    #endregion

    #region OverLoad

    bool overLoad = false;

    int chamberCountBeforeOverload = 0;

    [Space]
    [Header("OverLoad")]
    [Tooltip("Maximum Rounds Allowed to be Loaded In OverLoad")]
    [SerializeField] int overLoadThreshold = 3;

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

                    //Path Visualization

                    path.enabled = true;

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

            #region OverLoad

            if (action is ReloadAction && overLoad)
            {
                chamberCount = chamberCountBeforeOverload;

                InstantOverLoad();

                overLoad = false;
            }

            #endregion
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

                    ((inventory.ActiveEntry.Value.Item as Bow).liveBarrel.liveSlug as Projectile).speed = 
                    ((inventory.ActiveEntry.Value.Item as Bow).liveBarrel.liveSlug as Arrow).defaultSpeed;

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

            switch (state)
            {
                case GameConstants.AS_Idle:

                    Dettach();

                    Reload();

                    break;

                default:

                    break;
            }
        });
    }

    private void Start()
    {
        path.positionCount = pathSegment;
    }

    private void Update()
    {
        if (isEquipped)
        {
            if (animationController.state[GameConstants.RangedLayer] == GameConstants.AS_Aiming)
            {

                #region OverLoad

                if (Input.GetButtonDown(GameConstants.Use1))
                {
                    if (chamberCount >= overLoadThreshold)
                    {
                        Debug.LogError("OverLoad Limit Exceeded");
                    }

                    else
                    {
                        chamberCountBeforeOverload = chamberCount;

                        //Force Reload
                        actor.GetComponent<Animator>().SetTrigger(GameConstants.reloadHash);

                        overLoad = true;
                    }
                }

                #endregion

                #region Arrow Draw Speed Increase

                Arrow projectile = (inventory.ActiveEntry.Value.Item as Bow).liveBarrel.liveSlug as Arrow;

                if (aimed)
                {
                    speedMultiplier += (Time.time - startTime) * Time.deltaTime;

                    if (speedMultiplier > projectile.speedThreshold)
                    {
                        aimed = false;
                    }

                    projectile.speed = projectile.defaultSpeed + (speedMultiplier / projectile.speedFator);
                }

                #endregion

                #region Path Visualization

                Vector3 aimPosition = actor.controllerPack.GetController<CombatController>().aimPosition;

                Vector3 vo = (aimPosition - muzzle.position).normalized * projectile.range * projectile.speed;

                VisulaizePath(vo);

                #endregion

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

        //Destroy Dummy Arrow Instance
        Destroy(dummyArrowInstance);

        //Path Visualization

        path.enabled = false;
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

    /// <summary>
    /// Calculate Position Of Projectile In Time
    /// </summary>
    /// <param name="vo">Initial Velocity</param>
    /// <param name="time">Time Instant</param>
    /// <returns></returns>
    Vector3 PositionInTime(Vector3 vo, float time)
    {
        Vector3 vxz = vo;
        vxz.y = 0f;

        Vector3 result = muzzle.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + muzzle.position.y;

        result.y = sY;

        return result;
    }

    /// <summary>
    /// Visualize Line Renderer For Path
    /// </summary>
    /// <param name="vo">Initial Velocity</param>
    void VisulaizePath(Vector3 vo)
    {
        for (int i = 0; i < pathSegment; i++)
        {
            Vector3 pos = PositionInTime(vo, i / (float) pathSegment);

            path.SetPosition(i, pos);
        }
    }
}
