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

    #region Walking Value

    [Header("Walk Input")]
    [HideInInspector] public float play_Input;
    public Rigidbody2D player_Rb { get; private set; }
    public float speed;

    #endregion

    #region Jump Value

    [Header("Jump Input")]
    [HideInInspector] public bool isTouchingGround; // เชื่อมกับ Script PlayerJumpCol
    public float jumpPower;

    #endregion

    #region Throw Value
    private ThrownManager thrownManager;
    [HideInInspector] public bool canThrow_; //อย่าสับสับกับ canThrown ของตัว thrown manager
    public LanternCore lantern;

    #endregion

    void Awake()
    {
        #region Get the component Ref here

        lantern = GameObject.FindWithTag("Lantern").GetComponent<LanternCore>();
        player_Rb = GetComponent<Rigidbody2D>();
        thrownManager = GameObject.Find("[MANAGE] LanternThrownManager").GetComponent<ThrownManager>();

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
    #endregion
}
