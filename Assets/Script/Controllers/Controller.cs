using System;
using UnityEngine;

public abstract class Controller : MonoBehaviour, IController
{
    public ActionPack actionPack;

    public ControllerPack controllerPack;

    public Character controlledCharacter;
    
    protected void InitializeActionPack<A>() where A : ActionPack, new()
    {
        actionPack = new A();

        actionPack.actionController = this;

        actionPack.actor = controllerPack.controlledCharacter;

        actionPack.OnInitialize();
    }

    public abstract void OnInitialize();
}
