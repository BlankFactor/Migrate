using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalSelection : BEvent
{
    public NaturalSelection()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "被吃";
    }

    public override void Execute(LeaderBird _lb)
    {
        if (_lb.birds.Count >= 1)
        {
            int index = Random.Range(0, _lb.birds.Count);

            _lb.birds[index].Die(false);

            desc = "一只被吃了";
            illu = Resources.Load<Sprite>("test");
        }
        else {
            desc = "你溜了";
            illu = Resources.Load<Sprite>("test");
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
