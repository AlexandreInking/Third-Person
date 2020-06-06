using UnityEngine;

public class CrouchAction : Action
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
        locomotionController.crouching = true;
        animator.SetTrigger(GameConstants.crouchDownHash);
    }
}
