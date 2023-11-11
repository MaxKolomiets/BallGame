using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player _player;

    public void SetPlayer(Player player) {
        _player = player;
    }

    void LateUpdate()
    {
        if (_player.gameObject.transform.position.z - 5f != transform.position.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _player.gameObject.transform.position.z - 5f);
        }
        //if (_player.gameObject.transform.position != transform.position + new Vector3(5, 0, -5))
        //{
        //    transform.position = _player.gameObject.transform.position + new Vector3(5, 0, -3);
        //}
    }
}
