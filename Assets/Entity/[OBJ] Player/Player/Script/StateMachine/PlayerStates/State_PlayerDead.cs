using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerDead : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.player_Rb.velocity = Vector2.zero;
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
}

