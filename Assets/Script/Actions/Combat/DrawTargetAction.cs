using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTargetAction : Action
{
    CombatController combatController;

    PlayerInventory inventory;

    Texture2D crossHair;

    int crossHairSize;

    public override void OnInitialize()
    {
        combatController = actionPack.actionController as CombatController;

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventory.OnQuickSlotEquipped += (entry => 
        {
            if (entry.Value is RangedWeapon)
            {
                RangedWeaponFlavor flavor = entry.Value.flavor as RangedWeaponFlavor;

                crossHair = flavor.crosshairTexture;

                crossHairSize = flavor.crosshairSize;
            }
        });
    }

    public override void OnAction()
    {
        Draw();
    }

    void Draw()
    {
        Vector3 aimPositionOnScreen = Camera.main.WorldToScreenPoint(combatController.aimPosition);

        aimPositionOnScreen.y = Screen.height - aimPositionOnScreen.y;

        GUI.DrawTexture(new Rect(aimPositionOnScreen.x, aimPositionOnScreen.y,
                    crossHairSize, crossHairSize), crossHair);
    }
}
