using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : BEvent
{
    public Trap()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "陷阱";
    }

    public override void Execute(LeaderBird _lb)
    {
        bool escape = false;

        if (_lb.birds.Count >= 1) {
            escape = Random.Range(0, 1.0f) <= 0.25f ? true : false;
        }

        if (escape)
        {
            _lb.AddCoreEnergy(10);
            _lb.AddSatiety(0.2f);

            foreach (var i in _lb.birds)
            {
                i.AddCoreEnergy(10);
                i.AddSatiety(0.2f);
            }

            illu = Resources.Load<Sprite>("test");
            desc = "成功溜走";
        }
        else
        {
            EventRecorder.instance.Add_Poarching(1);

            illu = Resources.Load<Sprite>("test");
            desc = "陷阱 被抓";
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
