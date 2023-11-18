using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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