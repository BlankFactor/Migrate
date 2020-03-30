using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VastFoods : BEvent
{
    public VastFoods() {
        illu = Resources.Load<Sprite>("test");
        desc = "汇编是世界上最好的编程语言";
    }

    public override void Execute(LeaderBird _lb)
    {
        _lb.GetComponent<BBird>().SetRestoreEnergy(true);
    }

    public override string GetDesc()
    {
        return desc;
    }

    public override Sprite GetIllu()
    {
        return illu;
    }

    public override void Undo(LeaderBird _lb)
    {
        _lb.GetComponent<BBird>().SetRestoreEnergy(false);
    }
}
