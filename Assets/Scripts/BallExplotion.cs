using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallExplotion {
   private float _ballRadius;
   private Vector3 _ballPos;
   private const float  ExplotionMultiplier = 3f;

    public BallExplotion(Ball ball) {
        _ballRadius = ball.transform.localScale.x / 2;
        _ballPos = ball.transform.position;
    }
    public void Explotion() 
    {
        Dictionary<Vector2Int, Barrier> newList = new();
        foreach (var obj in LevelController.instance.AllBarrier)
        {
            if (Vector3.Distance(obj.Value.transform.position, _ballPos) <= (_ballRadius + ExplotionMultiplier))
            {
                obj.Value.DeleteSign();
            }
            else
            {
                newList.Add(obj.Key, obj.Value);
            }
        }
        LevelController.instance.SetNewBarrierList(newList);
        onExplotion?.Invoke();
    }

    public static Action onExplotion;
}