using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _scoreText;
    TextMeshProUGUI _scoreTMP;
    ScoreAnim _scoreAnim;
    private int _score = 0;
    
    void Start()
    {
        _scoreTMP = _scoreText.GetComponent<TextMeshProUGUI>();
        _scoreAnim = _scoreText.GetComponent<ScoreAnim>();
        _score = 0;
    }

    public void AddToScore(int points)
    {
        _score += points;
        _scoreTMP.text = ("Score " + _score);
        _scoreAnim.AddScoreAnim();
    }
}
