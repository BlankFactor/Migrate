﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController instance;

    private List<ColorSetter> colorSetters = new List<ColorSetter>();
    private List<LightColorSetter> lightSetters = new List<LightColorSetter>();
    private List<Firefly> fireflySetters = new List<Firefly>();

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
        foreach (var v in lightSetters)
            v.SetColor(_v);
        foreach (var v in fireflySetters)
            v.SetColor(_v);
    }

    public void AddColorSetter(ColorSetter _cs) {
        colorSetters.Add(_cs);
    }
    public void AddLightColorSetter(LightColorSetter _lcs) {
        lightSetters.Add(_lcs);
    }
    public void AddFireFly(Firefly _ff) {
        fireflySetters.Add(_ff);
    }
    public void ClearFireFly()
    {
        fireflySetters.Clear();
    }
}