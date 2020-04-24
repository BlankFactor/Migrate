using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foraging_HalfFull : BEvent
{
    public Foraging_HalfFull()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "觅食_少";
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
