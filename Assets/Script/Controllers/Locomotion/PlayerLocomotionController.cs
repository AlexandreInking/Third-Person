using UnityEngine;

//TODO: Scalable PlayerLocomotionActions (for everyone except controlled chars)
class PlayerLocomotionController : LocomotionController
{
    [HideInInspector] public Vector2 inputVector = Vector2.zero;
    [HideInInspector] public Vector2 rawInputVector = Vector2.zero;

    public override void OnInitialize()
    {
        InitializeActionPack<LocomotionActionPack>();

        actionPack.AddActionCompletedListener<GetAngleAction>(delegate (Action action)
        {
            //Direction calculation depends on Angle calculation
            actionPack.TakeAction<GetDirectionAction>();
        });

        actionPack.AddActionCompletedListener<SetAnimatorAction>(delegate (Action action)
        {
            //OnAir Check should be done after isGrounded animator parameter is set
            actionPack.TakeAction<OnAirAction>();
        });

        actionPack.AddActionCompletedListener<OnAirAction>(delegate (Action action)
        {
            //Getting fall displacement should be done after OnAir Check is done 
            actionPack.TakeAction<GetDropAction>();
        });
    }

    private void Start()
    {
        controllerPack.GetController<CombatController>()
            .actionPack.AddActionInitiatedListener<AimDownAction>(action =>
            {
                if (sprinting)
                    actionPack.TakeAction<StopSprintAction>();
            });

        controllerPack.GetController<PlayerInventoryController>().inventory
            .OnQuickSlotEquipped += (ActiveEntry => 
            {
                if (ActiveEntry.Value.Item is RangedWeapon)
                    actionPack.TakeAction<StrafeAction>();

            });

        controllerPack.GetController<PlayerInventoryController>().inventory
            .OnQuickSlotUnEquipped += delegate 
            {
                actionPack.TakeAction<FreeAction>();
            };
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxis(GameConstants.Horizontal), Input.GetAxis(GameConstants.Vertical));
        rawInputVector = new Vector2(Input.GetAxisRaw(GameConstants.Horizontal), Input.GetAxisRaw(GameConstants.Vertical));

        actionPack.TakeAction<ToggleButtonAction>();
        actionPack.TakeAction<GetAngleAction>();
        actionPack.TakeAction<GetRotationAction>();
        actionPack.TakeAction<OnSlopeForceAction>();
        actionPack.TakeAction<SetAnimatorAction>();
    }
}
