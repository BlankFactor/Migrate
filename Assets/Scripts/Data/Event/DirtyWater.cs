using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyWater : BEvent
{
    public DirtyWater()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "肮脏的水源";
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
            if (Random.Range(0, 1.0f) <= 0.6f) {
                _lb.birds[0].Die();
                count++;
                if (_lb.birds.Count == 0)
                    break;
            }
        }

        EventRecorder.instance.Add_DirtyWater(count);

        illu = Resources.Load<Sprite>("test");
        desc = "肮脏的水源 减少核心体力 : " + value + " 死了 " + count + " 只鸟";

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
