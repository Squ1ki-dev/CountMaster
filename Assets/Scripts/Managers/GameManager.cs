using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Begin,
    Play,
    Minigame,
    GameEnd,
    Win,
    Lose
}

public static partial class GameStateEvent
{
    public static event System.Action<GameState> OnChangeGameState;
    public static void Fire_OnChangeGameState(GameState gameState) { OnChangeGameState?.Invoke(gameState); }
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;
    }

    private void Start() => OnChangeGameState(GameState.Begin);

    private void OnChangeGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Begin:
                HandleBegin();
                break;

            case GameState.Play:
                HandlePlay();
                break;

            case GameState.Minigame:
                HandleMinigame();
                break;

            case GameState.GameEnd:
                HandleMinigame();
                break;

            case GameState.Win:
                HandleWin();
                break;

            case GameState.Lose:
                HandleLose();
                break;

            default:
                break;
        }
    }

    public void HandleBegin() => EventManager.Fire_OnBeginGame();

    public void HandlePlay()
    {
        EventManager.Fire_OnPlayGame();
        EventManager.Fire_OnStartMovement();
    }

    public void HandleMinigame() => EventManager.Fire_OnMiniGame();
    public void HandleWin() => EventManager.Fire_OnWin();
    public void HandleLose() => EventManager.Fire_OnLose();

    public void DecreasePlayerAmount()
    {
        StackController.playerChildCount--;
        PlayerCount.playerCount.text = StackController.playerChildCount.ToString();

        if (StackController.playerChildCount <= 0)
            OnChangeGameState(GameState.Lose);
    }

    private void OnDisable() => GameStateEvent.OnChangeGameState -= OnChangeGameState;
}