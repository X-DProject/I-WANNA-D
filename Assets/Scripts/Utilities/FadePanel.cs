using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tool.Module.Message;

public class FadePanel : MonoBehaviour
{
    private Image fadeImage;
    void Start()
    {
        fadeImage = GetComponent<Image>();

        EventHandler.FadeOutEvent += OnFadeOut;
        GameInstance.Connect("fade.out", OnFadeOutM);
        EventHandler.FadeInEvent += OnFadeIn;
    }

    void OnDestroy()
    {
        EventHandler.FadeOutEvent -= OnFadeOut;
        GameInstance.Disconnect("fade.out", OnFadeOutM);
        EventHandler.FadeInEvent -= OnFadeIn;
    }

    private void OnFadeOut(float duration)
    {
        var targetColor = new Color(0,0,0,1);
        fadeImage.DOColor( targetColor, duration);
    }
    private void OnFadeOutM(IMessage msg)
    {
        float duration = (float)msg.Data;
        var targetColor = new Color(0,0,0,1);
        fadeImage.DOColor( targetColor, duration);
    }

    private void OnFadeIn(float duration)
    {
        var targetColor = new Color(0,0,0,0);
        fadeImage.DOColor( targetColor, duration);
    }

}
