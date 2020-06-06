using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAimAction : Action
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
        //Detect for Aiming and Trigger Events
        Toggle<AimDownAction, AimFreeAction>(combatController.aiming, combatProfile.aimToggle, GameConstants.Fire2);
    }

    void Toggle<InitiatedAction, CompletedAction>(bool on, bool toggle, string button)
        where InitiatedAction : Action
        where CompletedAction : Action
    {
        if (on)
        {
            if (toggle)
            {
                if (Input.GetButtonDown(button))
                    actionPack.TakeAction<CompletedAction>();
            }

            else
            {
                if (Input.GetButtonUp(button))
                    actionPack.TakeAction<CompletedAction>();
            }
        }

        else
        {
            if (Input.GetButtonDown(button))
                actionPack.TakeAction<InitiatedAction>();
        }
    }
}