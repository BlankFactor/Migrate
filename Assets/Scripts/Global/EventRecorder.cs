using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRecorder : MonoBehaviour
{
    public static EventRecorder instance;

    public float count_Poarching=0;
    public float count_DirtyWater=0;
    public float count_LostTheWay=0;
    public float count_Hungry=0;
    public float count_NaturalSel = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Add_Poarching(float _v) { count_Poarching += _v; }
    public void Add_DirtyWater(float _v) { count_DirtyWater += _v; }
    public void Add_LoatTheWay(float _v) { count_LostTheWay += _v; }
    public void Add_Hungry(float _v) { count_Hungry += _v; }
    public void Add_NaturalSel(float _v) { count_NaturalSel += _v; }
}
