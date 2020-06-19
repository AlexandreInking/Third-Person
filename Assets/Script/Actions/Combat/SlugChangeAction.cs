using UnityEngine;

/// <summary>
/// Changes Slug For Poly Cannons with Slug Variation Example Bomb Arrow for Bow
/// </summary>
public class SlugChangeAction : Action
{
    PlayerInventory inventory;

    public override void OnInitialize()
    {
        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);

        int clipCount = (inventory.ActiveEntry.Value.Adapter as RangedAdapter).clipCount;

        (inventory.ActiveEntry.Value.Adapter as RangedAdapter).clipCount = 0;

        RangedWeapon weapon = inventory.ActiveEntry.Value.Item as RangedWeapon;

        inventory.LoadMagazine(weapon.liveBarrel.liveSlug, clipCount);

        weapon.liveBarrel.liveSlug = (weapon.liveBarrel as PolyBarrel).NextVariant();

        actionPack.TakeAction<ReloadAction>();
    }
}
