using UnityEngine;

public class StandAction : Action
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
        locomotionController.crouching = false;
        animator.SetTrigger(GameConstants.crouchFreeHash);
    }
}