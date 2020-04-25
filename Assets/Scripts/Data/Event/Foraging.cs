using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foraging : BEvent
{
    public Foraging()
    {
        illu = Resources.Load<Sprite>("Foraging");
        desc = "\"只要愿意花上足够多的时间,在此处寻找到食物也并不是很难的事情\"\n"
            + TextColorSetter.Green("你不再感到饥饿,核心将持续回复");
    }

    public override void Execute(LeaderBird _lb)
    {
        _lb.AddSatiety(0.75f);
        _lb.SetRestoreCoreEnergy(true);

        foreach (var i in _lb.birds)
        {
            i.AddSatiety(0.5f);
            i.SetRestoreCoreEnergy(true);
        }

        GUIController.instance.Display_Panel_EventDesc(GetIllu(),GetDesc());
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
        _lb.SetRestoreCoreEnergy(false);

        foreach (var i in _lb.birds)
        {
            i.SetRestoreCoreEnergy(false);
        }
    }
}
