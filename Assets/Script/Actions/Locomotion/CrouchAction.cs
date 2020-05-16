using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAction : LocomotionAction
{
    public CrouchAction(Animator animator, IController playerMovementController) 
        : base(animator : animator, playerMovementController : playerMovementController)
    {

    }

    public override void OnInitialize()
    {
        
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);
        (_playerMovementController as PlayerMovementController).crouching = true;
        _animator.SetTrigger(StaticVariables.CrouchDown);
    }
}
