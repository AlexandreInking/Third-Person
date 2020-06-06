using UnityEngine;

public class FreeAction : Action
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
        locomotionController.strafing = false;
        animator.SetTrigger(GameConstants.freeHash);
    }
}