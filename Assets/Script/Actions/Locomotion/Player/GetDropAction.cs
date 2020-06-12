using UnityEngine;

/// <summary>
/// Gets fall Displacement/Velocity
/// </summary>
public class GetDropAction : Action
{
    LocomotionController locomotionController;

    public override void OnInitialize()
    {
        locomotionController = actionPack.actionController as LocomotionController;
    }

    public override void OnAction()
    {
        if (!locomotionController.airborne)
            locomotionController.fallVelocity.y = 0f;

        else
            locomotionController.fallVelocity.y += Physics.gravity.y * Time.deltaTime;
    }
}