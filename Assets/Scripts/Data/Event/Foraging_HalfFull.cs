using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foraging_HalfFull : BEvent
{
    public Foraging_HalfFull()
    {
        illu = Resources.Load<Sprite>("Foraging_HalfFull");
        desc = "\"也许在这贫瘠之地,还有你生存的机会\"\n"
            + TextColorSetter.Green("你的饥饿感仍在持续但已好了许多,核心将缓慢回复");
    }

    public override void Execute(LeaderBird _lb)
    {
        _lb.AddSatiety(0.5f);
        _lb.SetRestoreCoreEnergy(true,0.5f);

        foreach (var i in _lb.birds)
        {
            i.AddSatiety(0.2f);
            i.SetRestoreCoreEnergy(true,0.5f);
        }

        GUIController.instance.Display_Panel_EventDesc(GetIllu(), GetDesc());
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
