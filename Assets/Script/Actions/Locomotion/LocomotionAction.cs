using UnityEngine;

public abstract class LocomotionAction : Action
{
    protected Animator _animator;
    protected Transform _characterTranform;
    protected CharacterController _characterController;
    protected IController _locomotionController;
    
    public LocomotionAction(Animator animator = null, Transform characterTransform = null, 
        CharacterController characterController = null, IController locomotionController = null)
    {
        _animator = animator;
        _characterTranform = characterTransform;
        _characterController = characterController;
        _locomotionController = locomotionController;
    }
}
