using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Barrier> _barriersPrefabs = new();
    [SerializeField] private Plane _planePrefab;
    [SerializeField] private GameObject _portalPrefab;

    private Plane _plane;
    private GameObject _portal;
    public static int LevelLenth => _levelLenth;
    [SerializeField] private static int _levelLenth = 80;
    [SerializeField] private int _startOffset = 15;
    [SerializeField] private int _finishOffset = 10;
    [SerializeField] private int _signPer10Unit = 20;

    private void CratePlane() {

        _plane = Instantiate(_planePrefab);
        _plane.transform.position = new(5, 0, _levelLenth / 2);
        _plane.transform.localScale = new(10, 1, _levelLenth);
    }
    private void CreatePortal() {
        _portal = Instantiate(_portalPrefab);
        _portal.transform.position = new(5, 0.001f, _levelLenth-5);
    }

    private Dictionary<Vector2Int, Barrier> CreateSigns() {
        Dictionary<Vector2Int, Barrier> allBarrier = new();
        for (int i = _startOffset; i < _levelLenth - _finishOffset; i++)
        {
            Vector2Int pos = new Vector2Int(5, Random.Range(_startOffset, _levelLenth - _finishOffset));
            if (!allBarrier.TryGetValue(pos, out _))
            {
                allBarrier.TryAdd(pos, Instantiate(_barriersPrefabs[Random.Range(0, _barriersPrefabs.Count)], transform));
            }
        }

        for (int i = 0; i < _signPer10Unit * GetRealLenth() / 10; i++)
        {
            Vector2Int pos = new Vector2Int(Random.Range(0, 10), Random.Range(_startOffset, _levelLenth - _finishOffset));
            if (!allBarrier.TryGetValue(pos, out _))
            {
                allBarrier.TryAdd(pos, Instantiate(_barriersPrefabs[Random.Range(0, _barriersPrefabs.Count)], transform));
            }

        }
        foreach (var item in allBarrier)
        {
            item.Value.transform.position = new Vector3(item.Key.x, 0, item.Key.y);
        }
        return allBarrier;
    }
    public Dictionary<Vector2Int, Barrier> CreateLevel()
    {
        CratePlane();
        CreatePortal();
        return CreateSigns();
    }
    private void OnDestroy()
    {
        Destroy(_plane.gameObject);
        Destroy(_portal.gameObject);
    }
    public float GetRealLenth (){
        return _levelLenth - (_startOffset + _finishOffset);
    }
}
