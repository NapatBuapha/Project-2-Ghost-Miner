using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerIdle : PlayerBaseState
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("testIdle");
        rb = player.player_Rb;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        rb.velocity = new Vector2(player.speed * player.play_Input, rb.velocity.y);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.play_Input = Input.GetAxis("Horizontal");

        if (player.walkCon)
        {
            player.SwitchState(player.state_PlayerWalk);
        }

        if (player.jumpCon)
        {
            player.SwitchState(player.state_PlayerJump);
        }

        if (player.thrownCon)
        {
            player.SwitchState(player.state_PlayerThrowing);
        }

        if (player.lampFreezeCon)
        {
            player.LampFreeze();
        }

        if (player.lampReturnCon)
        {
            player.ChargeToChangeLanternState();
        }
    }
}

