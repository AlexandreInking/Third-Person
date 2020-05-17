using UnityEngine;

public class AimDownAction : LocomotionAction
{
    LocomotionController locomotionController;

    public AimDownAction(Animator animator, IController locomotionController)
        : base(animator: animator, locomotionController: locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.aiming = true;
        _animator.SetTrigger(GameConstants.aimDownHah);
    }
}