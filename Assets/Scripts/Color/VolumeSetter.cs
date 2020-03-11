using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeSetter : MonoBehaviour
{
    public Volume volume;
    public Gradient weightGradient;

    void Start()
    {
        VolumeController.instance.AddVolume(this);
    }

    public void SetWeight(float _t) {
        Color c = weightGradient.Evaluate(_t);
        volume.weight = c.a;
    }
}
