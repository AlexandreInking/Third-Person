public abstract class Action : IAction
{
    #region ActionInitiated

    public delegate void ActionInitiated(Action action);

    public event ActionInitiated OnActionInitiated;

    public void TriggerActionInitiated(Action action)
    {
        OnActionInitiated?.Invoke(action);
    }

    #endregion

    #region ActionCompleted

    public delegate void ActionCompleted(Action action);

    public event ActionCompleted OnActionCompleted;

    public void TriggerActionCompleted(Action action)
    {
        OnActionCompleted?.Invoke(action);
    }

    #endregion

    public ActionPack actionPack;
    public Character actor;

    public abstract void OnInitialize();
    public abstract void OnAction();
}
