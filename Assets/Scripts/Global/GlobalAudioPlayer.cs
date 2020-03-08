using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioPlayer : MonoBehaviour
{
    public static GlobalAudioPlayer instnace;

    [Header("音频源")]
    public AudioSource audio_Music;
    public AudioSource audio_Sound;

    [Header("音频片")]
    public AudioClip sound_Bgm;

    private void Awake()
    {
        instnace = this;
    }

    public void Play_Bgm() {
        audio_Music.clip = sound_Bgm;
        audio_Music.Play();
    }
}
