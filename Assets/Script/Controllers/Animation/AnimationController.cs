using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : Controller
{

    #region StateInitialized

    public delegate void StateInitialized(string state);

    public event StateInitialized OnStateInitialized;

    public void TriggerStateInitialized(string state)
    {
        OnStateInitialized?.Invoke(state);
    }

    #endregion

    #region StateComplete

    public delegate void StateComplete(string state);

    public event StateComplete OnStateComplete;

    public void TriggerStateComplete(string state)
    {
        OnStateComplete?.Invoke(state);
    }

    #endregion

    /// <summary>
    /// Key is Layer Index and Value is State String
    /// </summary>
    public Dictionary<int, string> state = new Dictionary<int, string>();

    public override void OnInitialize()
    {
        foreach (int layer in GameConstants.STATES.Keys)
        {
            state.Add(layer, string.Empty);
        }

        InitializeActionPack<AnimationActionPack>();
    }

    private void Update()
    {
        actionPack.TakeAction<StateAction>();

        //Debug.LogError(state[GameConstants.RangedLayer]);
    }
}
