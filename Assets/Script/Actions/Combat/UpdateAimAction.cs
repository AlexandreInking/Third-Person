using UnityEngine;

//TODO: Fix Crosshair
/// <summary>
/// Updates Aim Position
/// </summary>
public class UpdateAimAction : Action
{
    CombatController combatController;

    CombatProfile combatProfile;

    public override void OnInitialize()
    {
        combatController = actionPack.actionController as CombatController;

        combatProfile = (actor.profile as CombatantProfile).combatProfile;
    }

    public override void OnAction()
    {
        GetAim();
    }


    /// <summary>
    /// Updates Aim Position
    /// </summary>
    void GetAim()
    {
        Vector3 aimOffset = new Vector3(combatProfile.aimOffsetX, combatProfile.aimOffsetY, 0);

        Ray ray = new Ray(Camera.main.transform.position + aimOffset, Camera.main.transform.forward);

        combatController.aimPosition = ray.GetPoint(combatProfile.aimDistance);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, combatProfile.aimDistance, combatProfile.aimLayers))
        {
            combatController.aimPosition = hit.point;
        }

    }
}
