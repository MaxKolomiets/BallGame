using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;
using TMPro; 



public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleExplotion _particlePrefab;

    private Player _player;
    private float _unitOffset = 3f;
    private Vector3 _startPos;
    private Vector3 _targetPos = new(5,2,LevelGenerator.LevelLenth);
    private float _moveTime = 2f;
    private bool _ballMove = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _player.transform.position + new Vector3(0,0,_player.transform.localScale.z/2+ _unitOffset);
        
    }

    public void SetPlayer(Player player) {
        _player = player;
    }
    public void IncreaseSize(float time) {
        transform.localScale += new Vector3(time, time, time);
    }
    public async void EndIncrease() {
        AudioController.instance.PlaySound(AudioType.BallThrow);
        await MoveBall();
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sign")
        {
            //Debug.Log(other.tag);
            _ballMove = false;

        }
    }
    private async Task MoveBall() {
        float elapsedTime = 0;
        float rotationSpeed = 36;
        GetComponent<SphereCollider>().radius = transform.localScale.x / 4;
        _startPos = transform.position;
        while (elapsedTime < _moveTime&& _ballMove) {
            float controlVal = elapsedTime / _moveTime;
            transform.position = Vector3.Lerp(_startPos, _targetPos, controlVal);
            transform.Rotate(rotationSpeed*Time.deltaTime*Mathf.Rad2Deg, 0, 0);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
        BallExplotion explotion = new(this);
        explotion.Explotion();
        AudioController.instance.PlaySound(AudioType.BallExplotion);
       var particle = Instantiate(_particlePrefab);
        particle.transform.position = transform.position;
        Destroy(this);
        await Task.Delay(700);
        Destroy(particle.gameObject);
    }
    public void OnDestroy()
    {
        Destroy(GetComponent<MeshFilter>().gameObject);
    }

}
