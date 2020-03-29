using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VastFoods : BEvent
{
    public override void Execute(LeaderBird _lb)
    {
        _lb.GetComponent<BBird>().SetRestoreEnergy(true);
    }

    public override void Undo(LeaderBird _lb)
    {
        _lb.GetComponent<BBird>().SetRestoreEnergy(false);
    }
}
