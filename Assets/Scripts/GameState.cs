using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IGameState  {
    public void StartTap();
    public void Press(Player player);
    public void FinishTap();
    public void Start();
    public void Exit();
}
public enum GameStateEnum
{
    WinOrLose, BallMove, BallThrow, Waiting
}

public class WinOrLoseState : IGameState
{
    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void FinishTap()
    {
        throw new NotImplementedException();
    }

    public void Press(Player player)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void StartTap()
    {
        throw new NotImplementedException();
    }
}
public class BallMoveState : IGameState
{
    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void FinishTap()
    {
        throw new NotImplementedException();
    }

    public void Press(Player player)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void StartTap()
    {
        throw new NotImplementedException();
    }
}
public class BallThrowState : IGameState
{
    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void FinishTap()
    {
        throw new NotImplementedException();
    }

    public void Press(Player player)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void StartTap()
    {
        throw new NotImplementedException();
    }
}
public class WaitingState : IGameState
{
    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void FinishTap()
    {
        throw new NotImplementedException();
    }

    public void Press(Player player)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void StartTap()
    {
        throw new NotImplementedException();
    }
}

public static class GameState 
{
    private static Dictionary<Type, IGameState> _gameStateMap = new();
    public static IGameState CurrentState => _currentState;

    private static IGameState _currentState;

    private static void InitStateMap() {
        _gameStateMap[typeof(WinOrLoseState)] = new WinOrLoseState();
        _gameStateMap[typeof(BallMoveState)] = new BallMoveState();
        _gameStateMap[typeof(BallThrowState)] = new BallThrowState();
        _gameStateMap[typeof(WaitingState)] = new WaitingState();
    }

    private static void SetState(IGameState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Start();
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
