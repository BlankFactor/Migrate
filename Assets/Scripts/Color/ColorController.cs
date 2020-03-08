using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController instance;

    private List<ColorSetter> colorSetters = new List<ColorSetter>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void SetColor(float _v) {
        foreach (var v in colorSetters)
            v.SetColor(_v);
    }

    public void AddColorSetter(ColorSetter _cs) {
        colorSetters.Add(_cs);
    }
}
