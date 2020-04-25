using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VastFoods : BEvent
{
    public VastFoods() {
        illu = Resources.Load<Sprite>("VastFood");
        desc = "\"你在丛林中发现了大量的食物,这足以让你填饱肚子\"\n"
            + TextColorSetter.Green("你的核心大幅回复且你不在感到饥饿");
    }

    public override void Execute(LeaderBird _lb)
    {
        _lb.AddCoreEnergy(50);
        _lb.ResetSatiety();
        foreach (var i in _lb.birds)
        {
            i.AddCoreEnergy(50);
            i.ResetSatiety();
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
       
    }
}
