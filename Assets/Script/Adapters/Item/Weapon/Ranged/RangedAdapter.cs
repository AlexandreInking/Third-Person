using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedAdapter : WeaponAdapter
{
    [Tooltip("Currently Inside Clip")]
    [HideInInspector] public int clipCount = 0;

    [Tooltip("Currently Inside Chamber")]
    [HideInInspector] public int chamberCount = 0;

    [Space]
    [Tooltip("Slug Exit Point")]
    public Transform muzzle;

    bool aimed;

    public override void Initialize()
    {
        base.Initialize();

        actor.OnAnimationEvent += (eventTag =>
        {
            if (!isEquipped)
            {
                return;
            }

            switch (eventTag)
            {
                case GameConstants.AE_Fire:

                    Fire();

                    break;

                case GameConstants.AE_Draw:

                    Draw();

                    break;

                default:

                    break;
            }
        });
    }

    protected abstract void Fire();

    protected abstract void Draw();

    public override void Equip()
    {
        base.Equip();

        transform.SetParent(inventory.leftHand);
    }

    public override void UnEquip()
    {
        base.UnEquip();

        transform.SetParent(inventory.rangedWeapon);
    }

    public void LoadClip(int rounds)
    {
        Barrel barrel = (inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel;

        int open = barrel.clipSize - clipCount;

        Magazine magazine = inventory.GetActiveMagazine();

        if (open <= 0)
        {
            Debug.Log("Clip Full");

            return;
        }

        if (rounds > open)
        {
            if (magazine.count >= open)
            {
                inventory.UnLoadActiveMagazine(open);

                clipCount += open;
            }

            else
            {
                inventory.UnLoadActiveMagazine(magazine.count);

                clipCount += magazine.count;
            }
        }

        else
        {
            if (magazine.count >= rounds)
            {
                inventory.UnLoadActiveMagazine(rounds);

                clipCount += rounds;
            }

            else
            {
                inventory.UnLoadActiveMagazine(magazine.count);

                clipCount += magazine.count;
            }
        }
    }

    public void FillClip()
    {
        LoadClip((inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel.clipSize);
    }

    public void UnLoadClip(int rounds)
    {
        if (clipCount <= 0)
        {
            Debug.Log("Clip Empty");

            return;
        }

        if (rounds > clipCount)
        {
            inventory.LoadActiveMagazine(clipCount);

            clipCount = 0;
        }

        else
        {
            inventory.LoadActiveMagazine(rounds);

            clipCount -= rounds;
        }
    }

    public void ClearClip()
    {
        UnLoadClip((inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel.clipSize);
    }

    public void LoadChamber(int rounds)
    {
        Barrel barrel = (inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel;

        int open = barrel.chamberSize - chamberCount;

        Magazine magazine = inventory.GetActiveMagazine();

        if (open <= 0)
        {
            Debug.Log("Chamber Full");

            return;
        }

        if (rounds > open)
        {
            if (magazine.count >= open)
            {
                inventory.UnLoadActiveMagazine(open);

                chamberCount += open;
            }

            else
            {
                inventory.UnLoadActiveMagazine(magazine.count);

                chamberCount += magazine.count;
            }
        }

        else
        {
            if (magazine.count >= rounds)
            {
                inventory.UnLoadActiveMagazine(rounds);

                chamberCount += rounds;
            }

            else
            {
                inventory.UnLoadActiveMagazine(magazine.count);

                chamberCount += magazine.count;
            }
        }
    }

    public void FillChamber()
    {
        LoadChamber((inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel.chamberSize);
    }

    public void UnLoadChamber(int rounds)
    {
        if (chamberCount <= 0)
        {
            Debug.Log("Chamber Empty");

            return;
        }

        if (rounds > chamberCount)
        {
            inventory.LoadActiveMagazine(chamberCount);

            chamberCount = 0;
        }

        else
        {
            inventory.LoadActiveMagazine(rounds);

            chamberCount -= rounds;
        }
    }

    public void ClearChamber()
    {
        UnLoadChamber((inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel.chamberSize);
    }

    public void Reload()
    {
        ClearChamber();
        ClearClip();

        FillChamber();
        FillClip();
    }

    public void InstantLoad()
    {
        Barrel barrel = (inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel;

        int instantLoad = barrel.instantLoad;

        int open = barrel.chamberSize - chamberCount;

        if (instantLoad > clipCount)
        {
            if (open > clipCount)
            {
                chamberCount += clipCount;

                clipCount = 0;
            }

            else
            {
                chamberCount = barrel.chamberSize;

                clipCount -= open;
            }
        }

        else
        {
            if (open > instantLoad)
            {
                chamberCount += instantLoad;

                clipCount -= instantLoad;
            }

            else
            {
                chamberCount = barrel.chamberSize;

                clipCount -= open;
            }
        }
    }

    /// <summary>
    /// Loads Chamber WithOut Considering Chamber Size and Instant Load
    /// </summary>
    /// <param name="rounds">RoundsTo Overload Chamber With</param>
    public void OverLoad(int rounds)
    {
        Magazine magazine = inventory.GetActiveMagazine();

        if (magazine.count >= rounds)
        {
            inventory.UnLoadActiveMagazine(rounds);

            chamberCount += rounds;
        }

        else
        {
            inventory.UnLoadActiveMagazine(magazine.count);

            chamberCount += magazine.count;
        }
    }

    /// <summary>
    /// OverLoads Chamber with Instant Load rounds
    /// </summary>
    public void InstantOverLoad()
    {
        OverLoad((inventory.ActiveEntry.Value.Item as RangedWeapon).liveBarrel.instantLoad);
    }
}
