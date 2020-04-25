using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureWater : BEvent
{
    public PureWater()
    {
        illu = Resources.Load<Sprite>("PureWater");
        desc = "\"每一滴干净的水,都希望有价值的离去\"\n"
                    + TextColorSetter.Green("你的核心回复的更加迅速");
    }

    public override void Execute(LeaderBird _lb)
    {
        _lb.SetRestoreCoreEnergy(true, 2.0f);
        foreach (var i in _lb.birds) {
            i.SetRestoreCoreEnergy(true, 2.0f);
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
