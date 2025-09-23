using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerKillingObj : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(col.gameObject);
            Debug.Log("Player Death");
            GameStateManager.ChangeGameState(GameState.GameOver);    
        }
    }
}
