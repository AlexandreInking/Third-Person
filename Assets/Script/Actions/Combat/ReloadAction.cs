using UnityEngine;

public class ReloadAction : Action
{
    Animator animator;

    PlayerInventory inventory;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        actor.OnAnimationEvent += (eventTag => 
        {
            switch (eventTag)
            {
                case GameConstants.AE_Reload:

                    Reload();

                    TriggerActionCompleted(this);

                    break;

                default:

                    break;
            }
        });
    }

    public override void OnAction()
    {
        RangedWeapon weapon = inventory.ActiveEntry.Value as RangedWeapon;

        if (weapon.clipCount == weapon.clipSize)
        {
            //Full Clip
            Debug.LogError("Full Clip");

            return;
        }

        if (weapon.magazine <= 0)
        {
            //eMPTY mAG
            Debug.LogError("Empty Magazine");

            return;
        }

        TriggerActionInitiated(this);

        animator.SetTrigger(GameConstants.reloadHash);
    }

    public void Reload()
    {
        RangedWeapon weapon = inventory.ActiveEntry.Value as RangedWeapon;

        //Remaining Clip Slots
        int open = weapon.clipSize - weapon.clipCount;

        if (weapon.magazine > open)
        {
            weapon.clipCount = weapon.clipSize;

            weapon.magazine -= open;
        }

        else
        {
            weapon.clipCount += weapon.magazine;

            weapon.magazine = 0;
        }
    }
}
