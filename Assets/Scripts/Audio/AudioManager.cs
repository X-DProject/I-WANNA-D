using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer mixer;


    public void OnEnable()
    {
        EventHandler.PlayBGMEvent += OnPlayBGMEvent;
        EventHandler.PlayFXEvent += OnPlayFXEvent;
    }

    public void OnDisable()
    {
        EventHandler.PlayBGMEvent -= OnPlayBGMEvent;
        EventHandler.PlayFXEvent -= OnPlayFXEvent;
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

    private void OnPlayBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnPlayFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
