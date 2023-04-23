using UnityEngine;
using System;

public static partial class EventManager
{
    // GameManager events
    public static event Action OnBeginGame;
    public static void Fire_OnBeginGame() { OnBeginGame?.Invoke(); }

    public static event Action OnPlayGame;
    public static void Fire_OnPlayGame() { OnPlayGame?.Invoke(); }

    public static event Action<Transform> OnGameEnd;
    public static void Fire_OnGameEnd(Transform transform) { OnGameEnd?.Invoke(transform); }

    public static event Action OnMiniGame;
    public static void Fire_OnMiniGame() { OnMiniGame?.Invoke(); }

    public static event Action OnWin;
    public static void Fire_OnWin() { OnWin?.Invoke(); }

    public static event Action OnLose;
    public static void Fire_OnLose() { OnLose?.Invoke(); }


    //
    public static event Action OnStartMovement;
    public static void Fire_OnStartMovement(){ OnStartMovement?.Invoke(); }

    public static event Action OnStopMovement;
    public static void Fire_OnStopMovement() { OnStopMovement?.Invoke(); }

    public static event Action OnFinishArea;
    public static void Fire_OnFinishArea() { OnFinishArea?.Invoke(); }

    public static event Action OnGameOver;
    public static void Fire_OnGameOver() { OnGameOver?.Invoke(); }

    public static event Action LevelCompleted;
    public static void NextLevel() { LevelCompleted?.Invoke(); }

}
