using UnityEngine;

//Aim Down
public class AimDownAction : Action
{
    Animator animator;

    AnimationController animationController;

    CombatController combatController;

    int aimDownHash;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        //Check Controller Initiation Order If Null Ref is Thrown
        animationController = actor.controllerPack.GetController<AnimationController>();

        combatController = actionPack.actionController as CombatController;

        aimDownHash = Animator.StringToHash(GameConstants.AimDown);
    }

    public override void OnAction()
    {
        switch (animationController.state[GameConstants.RangedLayer])
        {
            //Firing or Reloading
            case GameConstants.AS_AimFire:
                return;
            case GameConstants.AS_HipFire:
                return;
            case GameConstants.AS_Reload:
                return;
            default:
                break;
        }

        TriggerActionInitiated(this);

        animator.SetTrigger(aimDownHash);

        combatController.aiming = true;

        animator.SetBool(GameConstants.aimingHash, combatController.aiming);
    }
}
