using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessStatus : MonoBehaviour
{
    [SerializeField] private float darknessCountdown;
    [SerializeField] private float stayInDarkTime;
    [SerializeField] private LayerMask lightLayer;
    [SerializeField] private Collider2D col;
    public float darkness_Multiplier;
    public bool isInVoid;
    PlayerStateManager player;
    bool isDead;
    bool isInDark;
    float soundDelayed = 0f;


    void Start()
    {
        isDead = false;
        player = GetComponent<PlayerStateManager>();
        col = GetComponent<Collider2D>();
        ResetTimer();
    }
    void FixedUpdate()
    {
        if (isDead || !isInDark)
            return;
            
        if (stayInDarkTime < darknessCountdown)
        {
            stayInDarkTime += Time.deltaTime * darkness_Multiplier;
        }
        else
        {
            isDead = true;
            AudioManager.PlaySound(SoundType.DARKNESS_scream, 0.5f);
            player.SwitchState(player.state_PlayerDead);
            Debug.Log("Player Death");
            GameStateManager.ChangeGameState(GameState.GameOver);
        }
    }

    void Update()
    {
        if (isDead)
            return;

        if (col.IsTouchingLayers(lightLayer))
        {
            isInDark = false;
            ResetTimer();
        }
        else if (!col.IsTouchingLayers(lightLayer))
        {
            isInDark = true;
        }

        if (soundDelayed > 0)
        {
            soundDelayed -= Time.deltaTime;
        }
        else
        {
            if (isInDark || isInVoid)
            {
                AudioManager.PlaySound(SoundType.PLAYER_hearthBeats, 0.4f);
                soundDelayed = 1.2f;

            }
        }
        }
    

    public void ResetTimer()
    {
        if(!isInVoid)
        stayInDarkTime = 0;
    }

    
}
