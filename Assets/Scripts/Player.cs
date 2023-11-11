using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System;


public class Player : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _playerMoveDuration = 1.5f;
    private Ball _ball;
    private Vector3 CalculateNextPlayerStep() {
        Vector2 minPos = transform.position - transform.localScale / 2;
        Vector2 maxPos = transform.position + transform.localScale / 2 + new Vector3(0, 0, LevelGenerator.LevelLenth);
        List<Vector2> signsFrontPlayer = new();
        foreach (var item in LevelController.instance.AllBarrier)
        {
            if (item.Key.x >= minPos.x && item.Key.y >= minPos.y && item.Key.x <= maxPos.x && item.Key.x <= maxPos.x) {
                signsFrontPlayer.Add(item.Key);
            }
        }

        Vector2 signNearPlayer = new(1000,1000); 

        foreach (var item in signsFrontPlayer)
        {
            if (item.y< signNearPlayer.y) {
                signNearPlayer = item;
            }
        }
        if (signsFrontPlayer.Count > 0)
        {
            return signNearPlayer;
        }
        else {
            
            return new(5, LevelGenerator.LevelLenth + 5);
        }
        //return signsFrontPlayer.Count>0 ? signNearPlayer: new(5,LevelGenerator.LevelLenth+5);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            onEndGame?.Invoke(true);
        }
    }

    private async Task MovePlayer() {
        var pos = CalculateNextPlayerStep();
        float elapsedTime = 0;
        var startPos = transform.position;
        var targetPos = new Vector3 (5, transform.position.y, pos.y - 10);
        while (elapsedTime < _playerMoveDuration)
        {
            float controlVal = elapsedTime / _playerMoveDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, controlVal);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }
    public async void MovePlayerNew() {
        await MovePlayer();

    }
    void Update()
    {
        if (transform.localScale.x > 0) {
            onEndGame(false);
        }

        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {      
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        GameState.SetBallThrowState();
                        GameState.CurrentState.StartTap();
                        break;
                    case TouchPhase.Stationary:
                    case TouchPhase.Moved:
                        var a = Time.deltaTime;
                        transform.localScale -= new Vector3(a, a, a);
                        GameState.CurrentState.Press(this);
                        break;
                    case TouchPhase.Ended:
                        GameState.SetBallMoveState();
                        GameState.CurrentState.FinishTap();
                        break;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                _ball = Instantiate(_ballPrefab);
                _ball.SetPlayer(this);
                //GameState.SetBallThrowState();
                GameState.CurrentState.StartTap();
            }
            if (Input.GetMouseButton(0)) 
            {
                GameState.CurrentState.Press(this);
                var time = Time.deltaTime;
                _ball.IncreaseSize(time);
                
                transform.localScale -= new Vector3(time, time, time);
            }

        }
        if (Input.GetMouseButtonUp(0)) 
        {
            GameState.SetBallMoveState();
            GameState.CurrentState.FinishTap();
            _ball.EndIncrease();
        }
    }

    public static Action <bool>onEndGame;

    private void OnEnable()
    {
        BallExplotion.onExplotion += MovePlayerNew; 
    }
    private void OnDisable()
    {
        BallExplotion.onExplotion -= MovePlayerNew;

    }
}

