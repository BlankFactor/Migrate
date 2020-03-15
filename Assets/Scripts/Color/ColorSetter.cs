using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour,IObserver
{
    public Material material;
    public Gradient color;
    public string colorName;

    public virtual void Start()
    {
        ColorController.instance.AddObserver(this);
    }

    public void Respond(float _v)
    {
        material.SetColor(colorName, color.Evaluate(_v));
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
