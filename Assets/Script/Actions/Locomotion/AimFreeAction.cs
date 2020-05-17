using UnityEngine;

public class AimFreeAction : LocomotionAction
{
    LocomotionController locomotionController;

    public AimFreeAction(Animator animator, IController locomotionController)
        : base(animator: animator, locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.aiming = false;
        _animator.SetTrigger(GameConstants.aimFreeHash);
    }
}