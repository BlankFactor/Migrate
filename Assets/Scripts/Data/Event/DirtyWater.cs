using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyWater : BEvent
{
    public DirtyWater()
    {
        illu = Resources.Load<Sprite>("DirtyWater");
        desc = "\"喝水的湖泊中散发着刺鼻的味道,大量的废品漂浮在水中\"\n"
                    + TextColorSetter.Red("你感到不适,核心大幅减少");
    }

    public override void Execute(LeaderBird _lb)
    {
        float value = 0;

        if (_lb.cur_CoreEnergy >= 50.0f)
        {
            value = Random.Range(0, 50.0f) / 1.5f;
        }
        else {
            value = Random.Range(0, _lb.cur_CoreEnergy) / 2;
        }

        _lb.cur_CoreEnergy -= value;
        foreach (var i in _lb.birds)
        {
            i.cur_CoreEnergy -= value;
        }

        int count = 0;

        int temp = _lb.birds.Count;
        for (int i = 0; i < temp; i++) {
            if (Random.Range(0, 1.0f) <= 0.45f) {
                _lb.birds[0].Die(false);
                count++;
                if (_lb.birds.Count == 0)
                    break;
            }
        }

        EventRecorder.instance.Add_DirtyWater(count);

        if(count != 0)
        desc = "\"喝水的湖泊中散发着刺鼻的味道,大量的废品漂浮在水中\"\n"
            + TextColorSetter.Red("你感到不适,核心大幅减少\n" + "你失去了 "+count+" 位同伴");
        else
            desc = "\"喝水的湖泊中散发着刺鼻的味道,大量的废品漂浮在水中\"\n"
            + TextColorSetter.Red("你感到不适,核心大幅减少\n");

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
