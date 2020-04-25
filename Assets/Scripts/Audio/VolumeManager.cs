using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour,ISubject
{
    public static VolumeManager instance;

    void Awake()
    {
        instance = this;
    }

    public List<IObserver> obs = new List<IObserver>();

    public void AddObserver(IObserver _ob)
    {
        obs.Add(_ob);
    }

    public void NotifyObserver(float _v)
    {
        foreach (var i in obs) {
            i.Respond(_v);
        }
    }

    public void RemoveObserver(IObserver _ob)
    {
        obs.Remove(_ob);
    }
}
