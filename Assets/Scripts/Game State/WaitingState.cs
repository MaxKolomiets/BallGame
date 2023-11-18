using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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