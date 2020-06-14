using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    Animator animator;

    PlayerInventory inventory;

    int fireHash;

    AnimationController animationController;

    bool aimed = false;

    float shotFiredTime = float.MinValue;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        fireHash = Animator.StringToHash(GameConstants.Fire);

        animationController = actor.controllerPack.GetController<AnimationController>();

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        actor.OnAnimationEvent += (eventTag => 
        {
            switch (eventTag)
            {
                case GameConstants.AE_Fire:
                    
                    (inventory.ActiveEntry.Value.Adapter as RangedAdapter).clipCount--;

                    break;

                case GameConstants.AE_Aimed:

                    aimed = true;

                    break;

                default:
                    break;
            }
        });

        animationController.OnStateComplete += (state =>
        {
            if (state == GameConstants.AS_Aiming)
            {
                aimed = false;
            }
        });
    }

    public override void OnAction()
    {
        if (!inventory.ActiveEntry.Key.isEquipped)
        {
            //UnArmed
        }

        else
        {
            if (inventory.ActiveEntry.Value == null)
            {
                //UnArmed + Empty Slot
            }

            else
            {
                if (inventory.ActiveEntry.Value.Item is Weapon)
                {
                    Attack(inventory.ActiveEntry.Value.Item as Weapon);
                }

                else
                {
                    //Non Weapon Equipped
                }
            }
        }
    }

    void Attack(Weapon weapon)
    {
        if (weapon is RangedWeapon)
        {
            Fire(weapon as RangedWeapon);
        }
    }

    /// <summary>
    /// Ranged Attack
    /// </summary>
    /// <param name="weapon">Ranged Weapon</param>
    void Fire(RangedWeapon weapon)
    {
        switch (animationController.state[GameConstants.RangedLayer])
        {
            //Firing or Reloading
            case GameConstants.AS_AimFire:
                return;
            case GameConstants.AS_HipFire:
                return;
            case GameConstants.AS_Reload:
                return;
            case GameConstants.AS_Aiming:

                if (aimed)
                {
                    break;
                }

                else
                {
                    return;
                }

            default:
                break;
        }

        //Fire Rate Logic
        if (Time.time - shotFiredTime > weapon.fireRate)
        {
            if ((inventory.ActiveEntry.Value.Adapter as RangedAdapter).clipCount <= 0)
            {
                //Empty Clip

                actionPack.TakeAction<ReloadAction>();

                return;
            }

            TriggerActionInitiated(this);

            animator.SetTrigger(fireHash);

            shotFiredTime = Time.time;
        }
    }

    void Slash()
    {
        //Mellee??
    }
}
