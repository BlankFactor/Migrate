using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void AddObserver(IObserver _ob);
    void RemoveObserver(IObserver _ob);
    void NotifyObserver(float _v);
}
