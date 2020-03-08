using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    public Material material;
    public Gradient color;
    public string colorName;

    public virtual void Start()
    {
        ColorController.instance.AddColorSetter(this);
    }

    public void SetColor(float _v) {
        material.SetColor(colorName, color.Evaluate(_v));
    }
}
