using System;
using System.Collections.Generic;

public abstract class ActionSet
{
    public Controller actionController;
    public List<Action> actions = new List<Action>();
    public Character actor;

    protected void InitializeAction<A>(params object[] injections) where A : Action
    {
        Action action = (A) Activator.CreateInstance(typeof(A), injections);

        action.actionSet = this;

        action.actor = actionController.controllerSet.controlledCharacter;

        action.OnActionInitiated += ActionInitiated;
        action.OnActionCompleted += ActionCompleted;

        actions.Add(action);

        action.OnInitialize();
    }

    private void ActionInitiated(Action action)
    {
        actionController.controllerSet.controlledCharacter.TriggerActionInitiated(action);
    }

    private void ActionCompleted(Action action)
    {
        actionController.controllerSet.controlledCharacter.TriggerActionCompleted(action);
    }

    public abstract void OnInitialize();

    public T GetAction<T>() where T : Action
    {
        return (T)actions.Find(x => x is T);
    }

    public void TakeAction<T>() where T : Action
    {
        GetAction<T>().OnAction();
    }
}
