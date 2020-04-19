using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image core;
    public Image outline;

    public void Reflash(float _core,float _energy) {
        core.fillAmount = _core;
        outline.fillAmount = _energy;
    }
}
