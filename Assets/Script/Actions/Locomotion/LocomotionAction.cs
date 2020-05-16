using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocomotionAction : Action
{
    protected Animator _animator;
    protected Transform _playerTranform;
    protected CharacterController _playerController;
    protected IController _playerMovementController;
    
    public LocomotionAction(Animator animator = null, Transform playerTransform = null, 
        CharacterController playerController = null, IController playerMovementController = null)
    {
        _animator = animator;
        _playerTranform = playerTransform;
        _playerController = playerController;
        _playerMovementController = playerMovementController;
    }
}
