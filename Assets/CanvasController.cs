using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sizeText;
    [SerializeField] private TextMeshProUGUI _wilLoseText;

    // Start is called before thTextMeshProUGUIe first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void WinOrLose(bool win)
    {
        _wilLoseText.text = win ? "Win" : "Lose";

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
