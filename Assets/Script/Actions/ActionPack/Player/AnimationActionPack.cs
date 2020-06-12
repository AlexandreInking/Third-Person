using UnityEngine;

public class AnimationActionPack : ActionPack
{
    public override void OnInitialize()
    {
        //oREDER oF iNITIALIZATION Matters for referencing
        InitializeAction<StateAction>();
    }
}
