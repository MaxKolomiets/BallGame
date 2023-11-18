using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLine : MonoBehaviour
{
    private Transform _transform;
    public void SetBall(Transform transform)
    {
        _transform = transform;
    }
    void Update()
    {
        transform.localScale = new Vector3(_transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(5, 0.01f, _transform.position.z + 11);
    }
}
