﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioPlayer : MonoBehaviour
{
    public static GlobalAudioPlayer instance;

    [Header("音量设定")]
    public float max_BgmVolume = 1.0f;

    [Header("渐入渐出设定")]
    public float speedScale = 1;

    [Header("音频源")]
    public AudioSource audio_Music;
    public AudioSource audio_Sound;

    [Header("音频片")]
    public AudioClip sound_Bgm;
    public AudioClip sound_Bgm_End;
    public AudioClip sound_Bell;
    public AudioClip sound_Gunfire;
    public AudioClip sound_BirdDie;

    [Header("接受对象")]
    public AudioListener globalListener;
    public AudioListener birdListener;

    private void Awake()
    {
        instance = this;
    }

    public void Play_Bgm() {
        audio_Music.clip = sound_Bgm;
        audio_Music.Play();
    }

    public void Play_Bell() {
        audio_Sound.clip = sound_Bell;
        audio_Sound.Play();
    }

    public void Play_Gunfire() {
        audio_Sound.clip = sound_Gunfire;
        audio_Sound.Play();
    }

    public void Play_BirdDie() {
        audio_Sound.clip = sound_BirdDie;
        audio_Sound.Play();
    }

    public void StartBgmFadeOut() {
        StartCoroutine(BgmFadeOut());
    }

    public void ChangeToBirdListener(bool _b) {
        birdListener.enabled = _b;
        globalListener.enabled = !_b;
    }

    IEnumerator BgmFadeOut() {
        while (audio_Music.volume != 0) {
            float v = audio_Music.volume - (Time.fixedDeltaTime * speedScale);
            v = Mathf.Clamp(v, 0, max_BgmVolume);

            audio_Music.volume = v;
            yield return Time.deltaTime;
        }
        audio_Music.Stop();
    }

    public void StartBgmFadeIn()
    {
        StartCoroutine(BgmFadeIn());
    }

    public void SwitchBgm() {
        if (audio_Music.clip.Equals(sound_Bgm))
        {
            audio_Music.clip = sound_Bgm_End;
        }
        else {
            audio_Music.clip = sound_Bgm;
        }
    }

    IEnumerator BgmFadeIn()
    {
        audio_Music.Play();

        while (audio_Music.volume != max_BgmVolume)
        {
            float v = audio_Music.volume + (Time.deltaTime * speedScale);
            v = Mathf.Clamp(v, 0, max_BgmVolume);

            audio_Music.volume = v;
            yield return Time.deltaTime;
        }
    }
}
