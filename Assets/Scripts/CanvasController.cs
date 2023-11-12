using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sizeText;
    [SerializeField] private TextMeshProUGUI _wilLoseText;
    [SerializeField] public TextMeshProUGUI _record;
    [SerializeField] private Button _playAgainButton;

    private void Start()
    {
        _playAgainButton.gameObject.SetActive(false);
    }

    private void WinOrLose(bool win)
    {
        GameState.SetWinOrLoseState();
        Debug.Log("asdasdasd");
        _playAgainButton.gameObject.SetActive(true);
        Handheld.Vibrate();
        _wilLoseText.text = win ? "Win" : "Lose";

    }
    public void SetPlayerSize(float size) {
        _sizeText.text = "size " + (int)(size*20);
    }
    public void SetNewRecord() {
        _record.text = "new record!";
    }


    private void OnEnable()
    {
        Player.onEndGame += WinOrLose;
    }
    private void OnDisable()
    {
        Player.onEndGame -= WinOrLose;
    }
}

public static class WinOrLose {
    public static void EndGame(bool win) {
        if (win)
        {
            Debug.Log("WINNN");
        }
    }
}