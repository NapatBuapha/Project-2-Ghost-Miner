using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerWalk : PlayerBaseState
{
    Rigidbody2D rb;
    float footStepDelayed = 0.4f;
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetTrigger("Walk");
        rb = player.player_Rb;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        if (footStepDelayed > 0)
        {
            footStepDelayed -= Time.deltaTime;
        }
        else
        {
            AudioManager.PlaySound(SoundType.PLAYER_walk, 0.2f);
            footStepDelayed = 0.4f;
        }
        
        rb.velocity = new Vector2(player.speed * player.play_Input, rb.velocity.y);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.play_Input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(player.play_Input) == 0)
        {
            player.SwitchState(player.state_PlayerIdle);
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

