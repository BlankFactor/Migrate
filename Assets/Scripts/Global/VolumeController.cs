using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    public static VolumeController instance;

    public bool stop;
    public bool readyToStop;

    [Header("Volume")]
    private List<VolumeSetter> volumes = new List<VolumeSetter>();

    private void Awake()
    {
        instance = this;
    }

    public void SetWeight(float _t ) {
        if (stop) return;

        if (readyToStop && ((_t <= 0.1f) || (_t >= 0.7f)))
        {
            readyToStop = false;
            stop = true;
            return;
        }

        foreach (var v in volumes)
            v.SetWeight(_t);
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
}
