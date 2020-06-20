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

        RangedAdapter adapter = inventory.ActiveEntry.Value.Adapter as RangedAdapter;

        adapter.ClearChamber();

        adapter.ClearClip();

        RangedWeapon weapon = inventory.ActiveEntry.Value.Item as RangedWeapon;

        weapon.liveBarrel.liveSlug = (weapon.liveBarrel as PolyBarrel).NextVariant();

        actionPack.TakeAction<ReloadAction>();
    }
}
