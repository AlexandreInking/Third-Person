using System;
using System.Collections.Generic;
using static Action;

public abstract class ActionPack : IActionPack
{
    public Controller actionController;
    public List<Action> actions = new List<Action>();
    public Character actor;

    protected void InitializeAction<A>() where A : Action, new()
    {
        A action = new A();

        action.actionPack = this;

        action.actor = actionController.controllerPack.controlledCharacter;

        action.OnActionInitiated += ActionInitiated;
        action.OnActionCompleted += ActionCompleted;

        actions.Add(action);

        action.OnInitialize();
    }

    public abstract void OnInitialize();

    private void ActionInitiated(Action action)
    {
        actionController.controllerPack.controlledCharacter.TriggerActionInitiated(action);
    }

    private void ActionCompleted(Action action)
    {
        actionController.controllerPack.controlledCharacter.TriggerActionCompleted(action);
    }

    public void AddActionInitiatedListener<T>(ActionInitiated OnInitiated) where T : Action
    {
        GetAction<T>().OnActionInitiated += OnInitiated;
    }

    public void AddActionCompletedListener<T>(ActionCompleted OnCompleted) where T : Action
    {
        GetAction<T>().OnActionCompleted += OnCompleted;
    }

    public T GetAction<T>() where T : Action
    {
        return (T)actions.Find(x => x is T);
    }

    public void TakeAction<T>() where T : Action
    {
        GetAction<T>().OnAction();
    }
}
