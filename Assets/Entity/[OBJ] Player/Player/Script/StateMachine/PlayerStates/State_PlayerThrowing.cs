using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerThrowing : PlayerBaseState
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Test");
        player.StartThrown();
        rb = player.player_Rb;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {

        if (Input.GetMouseButtonUp(0))
        {
            player.SwitchState(player.state_PlayerIdle);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            player.SwitchState(player.state_PlayerIdle);
            player.EndThrown();
        }

        
    }
}

