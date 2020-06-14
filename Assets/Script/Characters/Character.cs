using UnityEngine;

public abstract class Character : MonoBehaviour, ICharacter
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

    #region OnAnimatorIk

    public delegate void AnimatorIk();

    public event AnimatorIk OnAnimatorIk;

    void TriggerOnAnimatorIk()
    {
        OnAnimatorIk?.Invoke();
    }

    #endregion

    #region AnimationEvent

    public delegate void AnimationEvent(string eventTag);

    public event AnimationEvent OnAnimationEvent;

    public void TriggerAnimationEvent(string eventTag)
    {
        OnAnimationEvent?.Invoke(eventTag);
    }

    #endregion

    public ControllerPack controllerPack;

    public CharacterProfile profile;

    protected void InitializeControllerPack<T>() where T : ControllerPack
    {
        GameObject obj = new GameObject(nameof(controllerPack), typeof(T));
        obj.transform.SetParent(transform);

        controllerPack = obj.GetComponent<T>();

        controllerPack.controlledCharacter = this;

        controllerPack.OnInitialize();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        TriggerOnAnimatorIk();
    }

    public void PromptAnimationEvent(string eventTag)
    {
        TriggerAnimationEvent(eventTag);
    }
}
