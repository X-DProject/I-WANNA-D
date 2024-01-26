using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public static class EventHandler 
{
    // 渐隐
    public static event Action<float> FadeOutEvent;
    public static void CallFadeOutEvent(float duration)
    {
        FadeOutEvent?.Invoke(duration);
    }
    // 渐现
    public static event Action<float> FadeInEvent;
    public static void CallFadeInEvent(float duration)
    {
        FadeInEvent?.Invoke(duration);
    }

    // 加载新场景
    public static event Action<AssetReference> LoadSceneEvent;
    public static void CallLoadSceneEvent(AssetReference loadScene)
    {
        LoadSceneEvent?.Invoke(loadScene);
    }

    // 播放音效
    public static event Action<AudioClip> PlayFXEvent;
    public static void CallPlayFXEvent(AudioClip clip)
    {
        PlayFXEvent?.Invoke(clip);
    }

    // 播放BGM
    public static event Action<AudioClip> PlayBGMEvent;
    public static void CallPlayBGMEvent(AudioClip clip)
    {
        PlayBGMEvent?.Invoke(clip);
    }
}