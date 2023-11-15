using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGeneratorPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private CanvasController _canvasPrefab;
    [SerializeField] private BallLine _linePrefab;
    private LevelGenerator _levelGenerator;
    private Player _player;
    private CanvasController _canvas; 
    public static LevelController instance = null;
    private BallLine _ballLine;
    private BallLine _playerLine;

    [SerializeField] private Ball _ballPrefab;
    private Ball _ball;

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
    public void CreateBall(Player player) {
        _ball = Instantiate(_ballPrefab);
        _ball.SetPlayer(player);
    }
    public void IncreaseBallSize() 
    {
        var time = Time.deltaTime;
        _ball.IncreaseSize(time);
    }
    public void DecreasePlayerSize(Player player) {
        var time = Time.deltaTime;
        player.transform.localScale -= new Vector3(time, time, time);
    }
    public void StartMoveBall() {
        _ball.EndIncrease();
    }
    private void InitializeManager()
    {
        _levelGenerator = Instantiate(_levelGeneratorPrefab);
        _allBarrier = _levelGenerator.CreateLevel();
        _player = Instantiate(_playerPrefab);
        _cameraController.SetPlayer(_player);
        _canvas = Instantiate(_canvasPrefab);
        GameState.SetDefoultValue();
    }
    public void SetNewBarrierList(Dictionary<Vector2Int, Barrier> newBarrierList)
    {
        _allBarrier = newBarrierList;
    }
    public void RestartGame() {
        if (GameState.IsCurrentStateIsWinOrLose()) {
            Destroy(_player);
            Destroy(_levelGenerator.gameObject);
            Destroy(_canvas.gameObject);
            if (_ball != null) {
                Destroy(_ball.gameObject);
            }
            if (_ballLine) {
                Destroy(_ballLine.gameObject);
            }

            foreach (var item in _allBarrier)
            {
                Destroy(item.Value.gameObject);
            }
            InitializeManager();
        }
    }

    public void CreateBallLine() {
        _ballLine = Instantiate(_linePrefab);
        _ballLine.SetBall(_ball.transform);
    }
    public void DeleteBallLine() {
        Destroy(_ballLine.gameObject);
    }

    public void CreatePlayerLine()
    {
        _playerLine = Instantiate(_linePrefab);
        _playerLine.SetBall(_player.transform);
    }
    public void DeletePlayerLine()
    {
        Destroy(_playerLine.gameObject);
    }

    public void Update()
    {
        if (_player) {
            _canvas.SetPlayerSize(_player.transform.localScale.x);
        }
    }
    private void EndGame(bool win)
    {
        //GameState.SetWinOrLoseState();
        AudioController.instance.PlaySound(win ? AudioType.Win : AudioType.Lose);
        int currentSize = (int) (20*_player.transform.localScale.x);
        if (RatingLoader.IsNewRecord(currentSize))
        {
            _canvas.SetNewRecord();
        }
    }



    private void OnEnable()
    {
        Player.onEndGame += EndGame;
    }
    private void OnDisable()
    {
        Player.onEndGame -= EndGame;
    }
}
