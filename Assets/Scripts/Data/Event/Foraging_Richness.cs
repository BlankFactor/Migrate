using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foraging_Richness : BEvent
{

    public Foraging_Richness()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "觅食_多";
    }

    public override void Execute(LeaderBird _lb)
    {
        _lb.AddSatiety(0.9f);
        _lb.SetRestoreCoreEnergy(true,1.5f);
        _lb.SetRestoreEnergy(true,2.0f);

        foreach (var i in _lb.birds) { 
            i.AddSatiety(0.9f);
            i.SetRestoreCoreEnergy(true,1.5f);
            i.SetRestoreEnergy(true, 2.0f);
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
        _lb.SetRestoreEnergy(false);

        foreach (var i in _lb.birds) {
            i.SetRestoreCoreEnergy(false);
            i.SetRestoreEnergy(false);
        }
    }
}
