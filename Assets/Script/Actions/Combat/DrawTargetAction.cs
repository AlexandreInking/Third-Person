using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTargetAction : Action
{
    CombatController combatController;

    public override void OnAction()
    {
        Draw();
    }

    public override void OnInitialize()
    {
        combatController = actionPack.actionController as CombatController;
    }

    void Draw()
    {
        Vector3 aimPositionOnScreen = Camera.main.WorldToScreenPoint(combatController.aimPosition);
        aimPositionOnScreen.y = Screen.height - aimPositionOnScreen.y;

        RangedWeaponFlavor rangedWeaponFlavor = actor.controllerPack
            .GetController<PlayerInventoryController>().inventory.ActiveEntry.Value.flavor as RangedWeaponFlavor;

        GUI.DrawTexture(new Rect(aimPositionOnScreen.x, aimPositionOnScreen.y,
                    rangedWeaponFlavor.crosshairSize, rangedWeaponFlavor.crosshairSize), rangedWeaponFlavor.crosshairTexture);
    }
}
