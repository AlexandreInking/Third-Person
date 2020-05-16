using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSet : ControllerSet
{
    private void Awake()
    {
        InitializeController<PlayerMovementController>();
    }
}
