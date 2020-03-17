using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightColorSetter : MonoBehaviour,IObserver
{
    public Light2D light_;
    public Gradient color;

    public virtual void Start()
    {
        ColorController.instance.AddObserver(this);
    }

    public void Respond(float _v)
    {
        light_.color = color.Evaluate(_v);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
