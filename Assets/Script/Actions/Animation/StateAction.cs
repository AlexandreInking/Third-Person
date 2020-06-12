
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateAction : Action
{
    Animator animator;

    AnimationController animationController;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        animationController = actionPack.actionController as AnimationController;

        foreach (int layer in animationController.state.Keys.ToList())
        {
            //Current Hash
            int hash = animator.GetCurrentAnimatorStateInfo(layer).shortNameHash;

            //Layer States
            GameConstants.STATES.TryGetValue(layer, out Dictionary<int, string> states);

            if (states.ContainsKey(hash))
            {
                animationController.state[layer] = states[hash];
            }
        }
    }

    public override void OnAction()
    {
        foreach (int layer in animationController.state.Keys.ToList())
        {
            //Current Hash
            int hash = animator.GetCurrentAnimatorStateInfo(layer).shortNameHash;

            GameConstants.STATES.TryGetValue(layer, out Dictionary<int, string> states);

            //Current State
            string state = string.Empty;

            if (states == null)
            {
                Debug.LogError($"Layer : {layer} States Dictionary Not Found");
                continue;
            }

            states.TryGetValue(hash, out state);

            //Check if State 
            if (state == string.Empty)
            {
                continue;
            }

            animationController.state.TryGetValue(layer, out string previousState);

            //Same State : No Changes
            if (state == previousState)
            {
                continue;
            }

            animationController.state[layer] = state;

            animationController.TriggerStateInitialized(state);

            if (previousState != null)
            {
                animationController.TriggerStateComplete(previousState);
            }
        }
    }
}
