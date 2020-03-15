using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour,ISubject
{
    public static ColorController instance;

    private List<IObserver> observers = new List<IObserver>();

    private void Awake()
    {
        instance = this;
    }

    public void AddObserver(IObserver _ob)
    {
        observers.Add(_ob);
    }

    public void RemoveObserver(IObserver _ob)
    {
        observers.Remove(_ob);
    }

    public void NotifyObserver(float _v)
    {
        foreach (var v in observers)
            v.Respond(_v);
    }

    public void ClearFirefly() {
        List<IObserver> temp = new List<IObserver>();

        foreach (var v in observers) {
            if (v.GetGameObject().tag.Equals("Firefly")) {
                temp.Add(v);
            }
        }

        foreach (var v in temp)
            RemoveObserver(v);

        foreach (var v in temp)
            Destroy(v.GetGameObject());
    }
}
