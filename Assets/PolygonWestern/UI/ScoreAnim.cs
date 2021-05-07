using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreAnim : MonoBehaviour
{

    private void Start()
    {
        AddScoreAnim();
    }

    public void AddScoreAnim()
    {
        transform.DOShakePosition(0.4f, 10, 15, 90, false, true);
    }

}
