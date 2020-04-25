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
        bool escape = true;

        if (_lb.birds.Count >= 1) {
            escape = Random.Range(0, 1.0f) <= 0.25f ? true : false;
        }
        Debug.Log(_lb.birds.Count);
        Debug.Log(escape);
        if (escape)
        {
            _lb.AddCoreEnergy(10);
            _lb.AddSatiety(0.2f);

            foreach (var i in _lb.birds)
            {
                i.AddCoreEnergy(10);
                i.AddSatiety(0.2f);
            }

            illu = Resources.Load<Sprite>("Trap_RunAway");
            desc = "\"迅捷的身体让你带着诱饵躲过了抓捕\"\n"
                + TextColorSetter.Green("回复少量核心且稍微填了肚子");
        }
        else
        {
            EventRecorder.instance.Add_Poarching(1);
            Debug.Log(2);
            illu = Resources.Load<Sprite>("Trap_Hit");
            desc = "\"零碎的食物,换走的却是生命\"\n"
                + TextColorSetter.Red("你的同伴陷入了陷阱之中");
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
