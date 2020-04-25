using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foraging_Richness : BEvent
{

    public Foraging_Richness()
    {
        illu = Resources.Load<Sprite>("Foraging_Richness");
        desc = "\"这里的食物是如此丰富,只需要花费一点时间,生存便如此简单\"\n" 
            + TextColorSetter.Green("你已经毫无饥饿感,体力和核心回复的更加迅速");
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
