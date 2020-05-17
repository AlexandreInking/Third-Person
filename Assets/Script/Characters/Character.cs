using UnityEngine;

public abstract class Character : MonoBehaviour
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


    public ControllerSet controllerSet;
    public CharacterProfile profile;

    protected void InitializeControllerSet<T>() where T : ControllerSet
    {
        GameObject obj = new GameObject(nameof(controllerSet), typeof(T));
        obj.transform.SetParent(transform);

        controllerSet = obj.GetComponent<T>();
        controllerSet.controlledCharacter = this;
    }
}
