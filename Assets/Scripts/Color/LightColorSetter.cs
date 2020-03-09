using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightColorSetter : MonoBehaviour
{
    public Light2D light_;
    public Gradient color;

    public virtual void Start()
    {
        ColorController.instance.AddLightColorSetter(this);
    }

    public void SetColor(float _v)
    {
        light_.color = color.Evaluate(_v);
    }
}
