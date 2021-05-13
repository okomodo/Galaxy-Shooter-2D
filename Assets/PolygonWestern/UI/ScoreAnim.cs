using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreAnim : MonoBehaviour
{
    private AudioSource _cashSFX;
    private void Start()
    {
        _cashSFX = GetComponent<AudioSource>();
    }

    public void AddScoreAnim()
    {
        _cashSFX.pitch = (Random.Range(0.75f, 1.25f));
        _cashSFX.Play();
        transform.DOShakePosition(0.4f, 10, 15, 90, false, true);
    }

}
