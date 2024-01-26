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
        GameInstance.Connect("fade.out", OnFadeOut);
        GameInstance.Connect("fade.in", OnFadeIn);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("fade.out", OnFadeIn);
        GameInstance.Disconnect("fade.in", OnFadeIn);
    }

    private void OnFadeOut(IMessage msg)
    {
        float duration = (float)msg.Data;
        var targetColor = new Color(0,0,0,1);
        fadeImage.DOColor( targetColor, duration);
    }

    private void OnFadeIn(IMessage msg)
    {
        float duration = (float)msg.Data;
        var targetColor = new Color(0,0,0,0);
        fadeImage.DOColor( targetColor, duration);
    }

}
