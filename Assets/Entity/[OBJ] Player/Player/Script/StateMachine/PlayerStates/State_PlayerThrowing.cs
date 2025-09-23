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
        rb.velocity = new Vector2(player.speed * player.play_Input, rb.velocity.y);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.play_Input = Input.GetAxis("Horizontal");

        if (Input.GetMouseButtonUp(0))
        {
            player.SwitchState(player.state_PlayerIdle);
        }

        if (player.walkCon)
        {
            player.SwitchState(player.state_PlayerWalk);
            player.EndThrown();
        }

        if (player.jumpCon)
        {
            player.SwitchState(player.state_PlayerJump);
            player.EndThrown();
        }
        
    }
}

