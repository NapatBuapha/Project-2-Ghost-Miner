using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerKillingObj : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStateManager player = col.GetComponent<PlayerStateManager>();
            player.SwitchState(player.state_PlayerDead);
            Debug.Log("Player Death");
            GameStateManager.ChangeGameState(GameState.GameOver); 
        }
    }
}
