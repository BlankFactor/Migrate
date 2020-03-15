using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour,ISubject
{
    public static VolumeController instance;

    public bool stop;
    public bool readyToStop;

    [Header("Volume")]
    private List<VolumeSetter> volumes = new List<VolumeSetter>();
    private List<IObserver> observers = new List<IObserver>();

    private void Awake()
    {
        instance = this;
    }

    // 设置可暂停 等待暂停时机
    public void SetStop(bool _v) {
        if (!_v)
        {
            stop = false;
        }

        readyToStop = _v;
    }

    public void AddVolume(VolumeSetter _v) {
        volumes.Add(_v);
    }

    public void RemoveVolume(VolumeSetter _v) {
        volumes.Remove(_v);
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

        foreach (var v in volumes)
            v.Respond(_v);
    }
}
