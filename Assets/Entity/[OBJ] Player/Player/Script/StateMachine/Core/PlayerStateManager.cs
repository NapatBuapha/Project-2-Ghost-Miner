using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStateManager : MonoBehaviour
{


    PlayerBaseState currentState;

    //Input Each State Here
    public State_PlayerIdle state_PlayerIdle { get; private set; } = new State_PlayerIdle();
    public State_PlayerWalk state_PlayerWalk { get; private set; } = new State_PlayerWalk();
    public State_PlayerJump state_PlayerJump { get; private set; } = new State_PlayerJump();

    public State_PlayerFalling state_PlayerFalling { get; private set; } = new State_PlayerFalling();
    public State_PlayerThrowing state_PlayerThrowing { get; private set; } = new State_PlayerThrowing();
    public State_PlayerLampDash state_PlayerLampDash { get; private set; } = new State_PlayerLampDash();

    #region Walking Value

    [Header("Walk Input")]
    [HideInInspector] public float play_Input;
    public Rigidbody2D player_Rb { get; private set; }
    public float speed;
    public bool walkCon { get; private set; }

    #endregion

    #region Jump Value

    [Header("Jump Input")]
    public bool isTouchingGround; // เชื่อมกับ Script PlayerJumpCol
    public float jumpPower;
    public float jumpAnimTime = 0.3f;
    public bool jumpCon { get; private set; }

    #endregion

    #region Throw Value
    private ThrownManager thrownManager;
    [HideInInspector] public bool canThrow_; //อย่าสับสบกับ canThrown ของตัว thrown manager
    public LanternCore lantern;
    public bool thrownCon { get; private set; }
    #endregion


    #region  LampDash Value
    [Header("LampDash Input")]
    public float dashPower;
    public bool dashCon { get; private set; }
    public float dashAnimTime = 0.5f;
    #endregion

    #region  LampControl Value
    [Header("LampControl Input")]
    public float maxLampReturnCharge;
    public bool lampFreezeCon { get; private set; }
    public bool lampReturnCon { get; private set; }
    public bool lampLightControlCon { get; private set; }
    public float lampReturnCharge { get; private set; }

    #endregion


    void Awake()
    {
        #region Get the component Ref here

        lantern = GameObject.FindWithTag("Lantern").GetComponent<LanternCore>();
        player_Rb = GetComponent<Rigidbody2D>();
        thrownManager = GameObject.Find("[MANAGE] LanternThrownManager").GetComponent<ThrownManager>();

        #endregion

        #region Set the variable
        lampReturnCharge = 0f;
        #endregion
    }

    #region StateMachineZone
    void Start()
    {
        SwitchState(state_PlayerIdle);
        currentState.EnterState(this);
    }

    void Update()
    {
        #region StateCondition
        walkCon = Mathf.Abs(play_Input) > 0;
        jumpCon = Input.GetKeyDown(KeyCode.Z) && isTouchingGround;
        thrownCon = Input.GetKeyDown(KeyCode.X) && !lantern.pickAble;
        dashCon = Input.GetKeyDown(KeyCode.C) && lantern.lanternState == LanternState.Floating && lantern.pickAble;
        #endregion

        #region NonStateCondition
        lampFreezeCon = Input.GetKeyDown(KeyCode.X) && lantern.pickAble && (lantern.lanternState != LanternState.Floating);
        lampReturnCon = Input.GetKey(KeyCode.X) && (lantern.lanternState == LanternState.Floating);
        lampLightControlCon = Input.GetKeyDown(KeyCode.S) && (lantern.lanternState == LanternState.Floating);
        #endregion

        //คำสั่งที่ใช้กับทุก State
        if (lampLightControlCon)
        {
            switch (lantern.lightState)
            {
                case LightState.Normal:
                    lantern.SwitchState(LightState.OverShine);
                    break;
                case LightState.OverShine:
                    lantern.SwitchState(LightState.Normal);
                    break;
            }
            
        }


        currentState.UpdateState(this);
    }


    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    #endregion

    #region Method Ref for Specific State

    public void StartThrown()
    {
        thrownManager.canThrown = true;
    }

    public void EndThrown()
    {
        thrownManager.CancleThrown();
    }

    public void LampFreeze()
    {
        lantern.SwitchState(LanternState.Floating);
    }

    public void ChargeToChangeLanternState()
    {
        if (lampReturnCharge < maxLampReturnCharge)
        {
            lampReturnCharge += Time.fixedDeltaTime;
        }
        else
        {
            lantern.SwitchState(LanternState.Returning);
            lampReturnCharge = 0;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            lampReturnCharge = 0;
        }
    }
    #endregion
}
