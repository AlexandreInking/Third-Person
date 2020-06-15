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
        Ray ray = Camera.main.ScreenPointToRay(CameraManager.Instance.CrossHair.transform.position);

        combatController.aimPosition = ray.GetPoint(combatProfile.aimDistance);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, combatProfile.aimDistance, combatProfile.aimLayers))
        {
            combatController.aimPosition = hit.point;
        }

    }
}
