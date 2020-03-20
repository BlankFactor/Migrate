using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeSetter : MonoBehaviour,IObserver
{
    public Volume volume;
    public Gradient weightGradient;

    void Start()
    {
        VolumeController.instance.AddObserver(this);
    }

    public void Respond(float _v)
    {
        Color c = weightGradient.Evaluate(_v);
        volume.weight = c.a;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
