using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer mixer;


    public void OnEnable()
    {
        GameInstance.Connect("fx.play", OnFxPlay);
        GameInstance.Connect("bgm.play", OnBGMPlay);
    }

    public void OnDisable()
    {
        GameInstance.Disconnect("fx.play", OnFxPlay);
        GameInstance.Disconnect("bgm.play", OnBGMPlay);
    }

    private void OnPauseEvent()
    {
        float amount;
        mixer.GetFloat("MasterVolume", out amount);

        // syncVolumeEvent.RaiseEvent(amount);
    }

    private void OnVolumeEvent(float amount)
    {
        mixer.SetFloat("MasterVolume", amount * 100 - 80);
    }

    private void OnBGMPlay(IMessage msg)
    {
        var clip = msg.Data as AudioClip;
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFxPlay(IMessage msg)
    {
        var clip = msg.Data as AudioClip;
        FXSource.clip = clip;
        FXSource.Play();
    }
}
