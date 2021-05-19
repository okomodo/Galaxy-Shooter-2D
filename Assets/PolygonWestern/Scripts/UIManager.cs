using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _scoreText;
    TextMeshProUGUI _scoreTMP;
    ScoreAnim _scoreAnim, _ammoAnim;
    private int _score = 0;

    [SerializeField] private GameObject _ammoText;
    TextMeshProUGUI _ammoTMP;

    void Start()
    {
        _scoreTMP = _scoreText.GetComponent<TextMeshProUGUI>();
        _scoreAnim = _scoreText.GetComponent<ScoreAnim>();
        _score = 0;

        _ammoTMP = _ammoText.GetComponent<TextMeshProUGUI>();
        _ammoAnim = _ammoText.GetComponent<ScoreAnim>();
    }

    public void AddToScore(int points)
    {
        _score += points;
        _scoreTMP.text = ("$" + _score);
        _scoreAnim.AddScoreAnim();
    }

    public void Ammo(int ammo)
    {
        _ammoTMP.text = ("Ammo: " + ammo);
        _ammoAnim.AddScoreAnim();
    }
}
