using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerLampDash : PlayerBaseState
{
    Rigidbody2D rb;
    Vector3 lanternPos;
    private float dashAnimTime;
    float baseGra;
    public override void EnterState(PlayerStateManager player)
    {
        
        rb = player.player_Rb;
        baseGra = rb.gravityScale;
        dashAnimTime = player.dashAnimTime;
        lanternPos = player.lantern.transform.position;
        Vector2 direction = lanternPos - player.transform.position;
        float distance = direction.magnitude;
        rb.gravityScale = 0;
        rb.AddForce(direction.normalized * (distance + player.dashPower), ForceMode2D.Impulse);
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.lantern.pickAble)
        {
            rb.gravityScale = baseGra;
        }

        if (dashAnimTime >= 0)
        {
            dashAnimTime -= Time.deltaTime;
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

