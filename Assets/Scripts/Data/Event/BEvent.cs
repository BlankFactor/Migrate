using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BEvent
{
    public Sprite illu;
    public string desc;

    public abstract void Execute(LeaderBird _lb);
    public abstract void Undo(LeaderBird _lb);

    public abstract Sprite GetIllu();
    public abstract string GetDesc();
}
