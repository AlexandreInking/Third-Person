using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTargetAction : Action
{
    CombatProfile combatProfile;

    PlayerInventory inventory;

    Sprite crossHair;

    int crossHairSize;

    public override void OnInitialize()
    {
        combatProfile = (actor.profile as CombatantProfile).combatProfile;

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        inventory.OnQuickSlotEquipped += (entry => 
        {
            if (entry.Value.Item is RangedWeapon)
            {
                CameraManager.Instance.CrossHair.enabled = true;

                RangedWeaponFlavor flavor = entry.Value.Item.flavor as RangedWeaponFlavor;

                crossHair = flavor.TargetSprite;

                crossHairSize = flavor.TargetSize;
            }
        });

        inventory.OnQuickSlotUnEquipped += (entry => 
        {
            if (entry.Value.Item is RangedWeapon)
            {
                CameraManager.Instance.CrossHair.enabled = false;
            }
        });
    }

    public override void OnAction()
    {
        Draw();
    }

    void Draw()
    {
        CameraManager.Instance.CrossHair.sprite = crossHair;

        CameraManager.Instance.CrossHair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, crossHairSize);
        CameraManager.Instance.CrossHair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, crossHairSize);

        CameraManager.Instance.CrossHair.transform.position = new Vector3(
            Screen.width * (0.5f + (combatProfile.aimOffsetX / 2)),
            Screen.height * (0.5f + (combatProfile.aimOffsetY / 2)),
            0);
    }
}
