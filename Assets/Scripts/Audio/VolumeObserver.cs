using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeObserver : MonoBehaviour,IObserver
{
    private AudioSource audio;
    private float initialVolume;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Respond(float _v)
    {
        audio.volume = initialVolume * _v;
    }

    void Start()
    {
        VolumeManager.instance.AddObserver(this);
        audio = GetComponent<AudioSource>();
        initialVolume = audio.volume;
    }
}
