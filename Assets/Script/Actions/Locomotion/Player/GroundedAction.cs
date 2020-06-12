
using UnityEngine;

public class GroundedAction : Action
{
    LocomotionController playerMovementController;

    Animator animator;

    public override void OnInitialize()
    {
        playerMovementController = actionPack.actionController as LocomotionController;

        animator = actor.GetComponent<Animator>();
    }

    public override void OnAction()
    {
        playerMovementController.airborne = false;

        animator.SetTrigger(GameConstants.landedHash);
    }
}