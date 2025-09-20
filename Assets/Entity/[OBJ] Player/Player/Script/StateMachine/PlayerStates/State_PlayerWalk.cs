using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerWalk : PlayerBaseState
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.player_Rb;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        rb.velocity = new Vector2(player.speed * player.play_Input, rb.velocity.y);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.play_Input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(player.play_Input) == 0)
        {
            player.SwitchState(player.state_PlayerIdle);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.SwitchState(player.state_PlayerJump);
        }

        if (Input.GetKeyDown(KeyCode.X) && !player.lantern.pickAble)
        {
            player.SwitchState(player.state_PlayerThrowing);
        }
    }
}

