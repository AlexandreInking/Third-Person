using UnityEngine;


//TODO: Scalable PlayerLocomotionActions (for everyone except controlled chars)
class PlayerLocomotionController : LocomotionController
{
    [HideInInspector] public Vector2 inputVector = Vector2.zero;
    [HideInInspector] public Vector2 rawInputVector = Vector2.zero;

    private void Start()
    {
        InitializeActionSet<PlayerMovementActionSet>();

        actionSet.GetAction<GetAngleAction>().OnActionCompleted += GetDirection;
        actionSet.GetAction<SetAnimatorAction>().OnActionCompleted += OnAir;
        actionSet.GetAction<OnAirAction>().OnActionCompleted += GetDrop;
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxis(GameConstants.Horizontal), Input.GetAxis(GameConstants.Vertical));
        rawInputVector = new Vector2(Input.GetAxisRaw(GameConstants.Horizontal), Input.GetAxisRaw(GameConstants.Vertical));

        actionSet.TakeAction<ToggleButtonAction>();
        actionSet.TakeAction<GetAngleAction>();
        actionSet.TakeAction<GetRotationAction>();
        actionSet.TakeAction<OnSlopeForceAction>();
        actionSet.TakeAction<SetAnimatorAction>();
    }

    private void GetDrop(Action action)
    {
        //Getting fall displacement should be done after OnAir Check is done 
        actionSet.TakeAction<GetDropAction>();
    }

    private void OnAir(Action action)
    {
        //OnAir Check should be done after isGrounded animator parameter is set
        actionSet.TakeAction<OnAirAction>();
    }

    private void GetDirection(Action action)
    {
        //Direction calculation depends on Angle calculation
        actionSet.TakeAction<GetDirectionAction>();
    }
}
