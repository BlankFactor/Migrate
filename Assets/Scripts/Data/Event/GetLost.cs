using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLost : BEvent
{
    public GetLost()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "迷路";
    }

    public override void Execute(LeaderBird _lb)
    {
        int count = 0;

        int temp = _lb.birds.Count;
        for (int i = 0; i < temp; i++) {

            // 40% 概率丢失一只鸟
            if (Random.Range(0, 1.0f) <= 0.4f) {
                _lb.birds[0].Die();
                count++;

                if (_lb.birds.Count == 0) {
                    break;
                }
            }
        }

        illu = Resources.Load<Sprite>("test");
        desc = count + " 只鸟不知生死";

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
