using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void Respond(float _v);
    GameObject GetGameObject();
}
