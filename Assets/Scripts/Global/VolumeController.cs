﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour,ISubject
{
    public static VolumeController instance;

    public bool stop;
    public bool readyToStop;

    [Header("Volume")]
    private List<IObserver> observers = new List<IObserver>();

    //***Temp
    public Volume Bloom_Night;
    public bool clearing;
    //***

    private void Awake()
    {
        instance = this;
    }

    void Update() {
        // *** Temp
        if (readyToStop && clearing) {
            Bloom_Night.weight -= WorldTimeManager.instance.NormalizeDeltaTime() * 20f;

            Bloom_Night.weight = Mathf.Clamp(Bloom_Night.weight, 0, 1);
            if (Bloom_Night.weight == 0) {
                clearing = false;
            }
        }
        // ***
    }

    // 设置可暂停 等待暂停时机
    public void SetStop(bool _v) {
        if (!_v)
        {
            stop = false;
        }

        readyToStop = _v;

        //***Temp
        clearing = true;
        //***
    }

    public void AddObserver(IObserver _ob)
    {
        observers.Add(_ob);
    }

    public void RemoveObserver(IObserver _ob)
    {
        observers.Remove(_ob);
    }

    public void NotifyObserver(float _v)
    {
        if (stop) return;

        if (readyToStop && ((_v <= 0.1f) || (_v >= 0.7f)))
        {
            readyToStop = false;
            stop = true;
            return;
        }

        foreach (var v in observers)
            v.Respond(_v);
    }
}
