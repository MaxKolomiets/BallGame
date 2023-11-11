using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGeneratorPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private CameraController _cameraController;
    private Player _player;
    public static LevelController instance = null;
    public Dictionary<Vector2Int, Barrier> AllBarrier => _allBarrier;
    private Dictionary<Vector2Int, Barrier> _allBarrier = new();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }
    private void InitializeManager()
    {
        var levelGenerator = Instantiate(_levelGeneratorPrefab);
        _allBarrier = levelGenerator.CreateLevel();
        _player = Instantiate(_playerPrefab);
        _cameraController.SetPlayer(_player);
    }
    public void SetNewBarrierList(Dictionary<Vector2Int, Barrier> newBarrierList)
    {
        _allBarrier = newBarrierList;
    }

}
