using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIFade : MonoBehaviour
{
    private bool _animStarted = false;
    private AudioSource _ricochetAudio;

    private void Start()
    {
        _ricochetAudio = GetComponent<AudioSource>();
        AddScoreAnim();
    }

    public void AddScoreAnim()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        if (_animStarted == false)
        {
            _animStarted = true;
            transform.DOPunchPosition(new Vector3(0, -50, 0), 1, 10, 0.5f, true);
            _ricochetAudio.pitch = (Random.Range(0.75f,1.25f));
            _ricochetAudio.Play();
            yield return new WaitForSeconds(1);
            _animStarted = false;
        }
    }
}
