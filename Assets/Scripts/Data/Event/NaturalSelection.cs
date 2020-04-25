using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalSelection : BEvent
{
    public NaturalSelection()
    {
        illu = Resources.Load<Sprite>("NaturalSelection_Die");
        desc = "物竞天择";
    }

    public override void Execute(LeaderBird _lb)
    {
        if (_lb.birds.Count >= 1)
        {
            int index = Random.Range(0, _lb.birds.Count);

            _lb.birds[index].Die(false);

            desc = "\"适者只能活着,强者才能生存\"\n"
                + TextColorSetter.Red("你的一位同伴丧生了");
            illu = Resources.Load<Sprite>("NaturalSelection_Die");

            EventRecorder.instance.Add_NaturalSel(1);
        }
        else {
            desc = "\"敏锐的洞察力也许会救你一命\"\n"
                + TextColorSetter.Green("你的一位同伴死里逃生");
            illu = Resources.Load<Sprite>("NaturalSelection_RunAway");
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
