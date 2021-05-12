using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{

    private bool _animStarted = false;
    private AudioSource _ricochetAudio;

    public void AddScoreAnim()
    {
        _ricochetAudio = GetComponent<AudioSource>();
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        if (_animStarted == false)
        {
            _animStarted = true;
            transform.DOPunchPosition(new Vector3(0, -50, 0), 1, 10, 0.5f, true);
            _ricochetAudio.pitch = (Random.Range(0.75f, 1.25f));
            _ricochetAudio.Play();
            yield return new WaitForSeconds(1);
            _animStarted = false;
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);

    }

    public void LoadSceneInstant()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        StartCoroutine(ExitGameRoutine());
    }

    IEnumerator ExitGameRoutine()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Quit Game");

    }
}
