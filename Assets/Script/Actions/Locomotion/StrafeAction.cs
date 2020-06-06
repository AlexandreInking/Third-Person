using UnityEngine;

public class StrafeAction : Action
{
    LocomotionController locomotionController;
    Animator animator;

    public override void OnInitialize()
    {
        locomotionController = actionPack.actionController as LocomotionController;
        animator = actor.GetComponent<Animator>();
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        locomotionController.strafing = true;
        animator.SetTrigger(GameConstants.strafeHah);
    }
}