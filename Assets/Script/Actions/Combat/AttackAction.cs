using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    Animator animator;
    int fireHash;

    public override void OnAction()
    {
        if (Input.GetButtonDown(GameConstants.Fire1))
        {
            TriggerActionInitiated(this);
            animator.SetTrigger(fireHash);
        }
    }

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();
        fireHash = Animator.StringToHash(GameConstants.Fire);
    }
}
