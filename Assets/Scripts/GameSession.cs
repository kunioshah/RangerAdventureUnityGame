using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private GameState gameState = GameState.Manual;
    private int checkPoint = 0;
    private int dialogCounter = 0;

    private void SetUpSingletion()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetCheckpoint(int point)
    {
        checkPoint = point;
    }

    public int GetCheckpoint()
    {
        return checkPoint;
    }

    public void SetDialogCounter(int counter)
    {
        dialogCounter = counter;
    }

    public int GetDialogCounter()
    {
        return dialogCounter;
    }

    private void Awake()
    {
        SetUpSingletion();
    }

}
