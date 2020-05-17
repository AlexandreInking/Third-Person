using UnityEngine;

public class CrouchAction : LocomotionAction
{
    LocomotionController locomotionController;

    public CrouchAction(Animator animator, IController locomotionController) 
        : base(animator : animator, locomotionController : locomotionController)
    {

    }

    public override void OnInitialize()
    {
        locomotionController = _locomotionController as LocomotionController;
    }

    public override void OnAction()
    {
        locomotionController.crouching = true;
        _animator.SetTrigger(GameConstants.crouchDownHash);
    }
}
