using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    Normal,
    Pause,
    GameOver
}
public class GameStateManager : MonoBehaviour
{
    GameState currentState;
    private static GameStateManager instance;

    #region GameOverValueRef
    [Header("Game Over Ref")]
    [SerializeField] private GameObject goCanvas;
    #endregion
    void Awake()
    {
        instance = this;
    }
    public static void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Pause:
                // หยุดเกม
                break;
            case GameState.GameOver:
                instance.goCanvas.GetComponent<GameOverUi>().OpenPanel();
                break;
            default:
                //
                break;
        }
    }
}
