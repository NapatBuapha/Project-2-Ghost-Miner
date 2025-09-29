using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerJump : PlayerBaseState
{
    Rigidbody2D rb;
    private float jumpAnimTime;
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetTrigger("Jump");
        jumpAnimTime = player.jumpAnimTime;
        rb = player.player_Rb;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        rb.velocity = new Vector2(player.speed * player.play_Input, rb.velocity.y);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.play_Input = Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }

        if (player.lampFreezeCon)
        {
            player.LampFreeze();
        }

        if (jumpAnimTime >= 0)
            {
                jumpAnimTime -= Time.deltaTime;
            }
            else
            {
                if (player.isTouchingGround)
                {
                    player.SwitchState(player.state_PlayerIdle);
                }
                else
                {
                    player.SwitchState(player.state_PlayerFalling);
                }
            }
        
    }
}

