using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IGameState  {
    public void StartTap(Player player);
    public void Press(Player player);
    public void FinishTap();
    public void Start();
}
public enum GameStateEnum
{
    WinOrLose, BallMove, BallThrow, Waiting
}

public class WinOrLoseState : IGameState
{

    public void FinishTap()
    {
    }

    public void Press(Player player)
    {
    }

    public void Start()
    {
    }

    public void StartTap(Player player)
    {
    }
}
public class BallMoveState : IGameState
{
    public void FinishTap()
    {
    }

    public void Press(Player player)
    {
    }

    public void Start()
    {
       
    }

    public void StartTap(Player player)
    {
    }
}
public class BallThrowState : IGameState
{
    public void FinishTap()
    {
        GameState.SetBallMoveState();
        LevelController.instance.StartMoveBall();
        LevelController.instance.DeleteBallLine(); 
    }

    public void Press(Player player)
    {
        LevelController.instance.IncreaseBallSize();
        LevelController.instance.DecreasePlayerSize(player);
    }

    public void Start()
    { 
    }

    public void StartTap(Player player)
    {
    }
}
public class WaitingState : IGameState
{

    public void FinishTap()
    {
       
    }

    public void Press(Player player)
    {
    }

    public void Start()
    {
        LevelController.instance.CreatePlayerLine();
    }

    public void StartTap(Player player)
    {
        LevelController.instance.CreateBall(player);
        LevelController.instance.CreateBallLine();
        LevelController.instance.DeletePlayerLine();

        GameState.SetBallThrowState();
    }
}

public static class GameState 
{
    private static Dictionary<Type, IGameState> _gameStateMap = new();
    public static IGameState CurrentState => _currentState;

    private static IGameState _currentState;

    public static void SetDefoultValue() {
        SetWaitingState();
    }
     static  GameState() {
        _gameStateMap[typeof(WinOrLoseState)] = new WinOrLoseState();
        _gameStateMap[typeof(BallMoveState)] = new BallMoveState();
        _gameStateMap[typeof(BallThrowState)] = new BallThrowState();
        _gameStateMap[typeof(WaitingState)] = new WaitingState();
    }

    private static void SetState(IGameState newState)
    {
        if (_currentState != null)
        {
        }
        _currentState = newState;
        _currentState.Start();
    }
    public static bool IsCurrentStateIsWinOrLose() {
        return _currentState == GetState<WinOrLoseState>();
    }

    private static IGameState GetState<T>() where T : IGameState
    {
        return _gameStateMap[typeof(T)];
    }
    public static void SetWinOrLoseState()
    {
        SetState(GetState<WinOrLoseState>());
    }
    public static void SetBallMoveState()
    {
        SetState(GetState<BallMoveState>());
    }
    public static void SetBallThrowState()
    {
        SetState(GetState<BallThrowState>());
    }
    public static void SetWaitingState()
    {
        SetState(GetState<WaitingState>());
    }
}
