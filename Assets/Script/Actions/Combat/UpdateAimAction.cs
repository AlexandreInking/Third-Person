using UnityEngine;

public class UpdateAimAction : Action
{
    CombatController combatController;
    CombatProfile combatProfile;

    public override void OnAction()
    {
        UpdateAimPosition();
    }

    public override void OnInitialize()
    {
        combatController = actionPack.actionController as CombatController;
        combatProfile = (actor.profile as CombatantProfile).combatProfile;
    }

    void UpdateAimPosition()
    {
        Vector3 aimOffset = new Vector3(combatProfile.aimOffsetX, combatProfile.aimOffsetY, 0);

        Ray ray = new Ray(Camera.main.transform.position + aimOffset, Camera.main.transform.forward);
        combatController.aimPosition = ray.GetPoint(combatProfile.aimDistance);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, combatProfile.aimDistance, combatProfile.aimLayers))
            combatController.aimPosition = hit.point;

    }
}
