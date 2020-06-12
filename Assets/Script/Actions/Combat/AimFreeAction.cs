using UnityEngine;

//Aim Free
public class AimFreeAction : Action
{
    Animator animator;

    AnimationController animationController;

    CombatController combatController;

    int aimFreeHash;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        animationController = actor.controllerPack.GetController<AnimationController>();

        combatController = actionPack.actionController as CombatController;

        aimFreeHash = Animator.StringToHash(GameConstants.AimFree);
    }

    public override void OnAction()
    {
        TriggerActionInitiated(this);

        combatController.aiming = false;

        animator.SetBool(GameConstants.aimingHash, combatController.aiming);

        switch (animationController.state[GameConstants.RangedLayer])
        {
            //Firing or Reloading
            case GameConstants.AS_Aiming:
                break;
            default:
                return;
        }

        animator.SetTrigger(aimFreeHash);
    }
}
