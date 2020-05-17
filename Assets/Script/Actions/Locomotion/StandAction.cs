using UnityEngine;

public class StandAction : LocomotionAction
{
    LocomotionController locomotionController;

    public StandAction(Animator animator, IController locomotionController)
        : base(animator: animator, locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.crouching = false;
        _animator.SetTrigger(GameConstants.crouchFreeHash);
    }
}