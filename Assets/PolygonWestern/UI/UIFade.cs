using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    private void Start()
    {
        Image image = GetComponent<Image>();
        Tweener(image);
    }

    static void Tweener(Image Tween)
    {
        DOTween.ToAlpha(() => Tween.color, x => Tween.color = x, 15, 10);
    }
}
