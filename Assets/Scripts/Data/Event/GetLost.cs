using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLost : BEvent
{
    public GetLost()
    {
        illu = Resources.Load<Sprite>("GetLost");
        desc = "GetLost";
    }

    public override void Execute(LeaderBird _lb)
    {
        int count = 0;

        int temp = _lb.birds.Count;
        for (int i = 0; i < temp; i++) {

            // 40% 概率丢失一只鸟
            if (Random.Range(0, 1.0f) <= 0.4f) {
                _lb.birds[0].Die(false);
                count++;

                if (_lb.birds.Count == 0) {
                    break;
                }
            }
        }

        EventRecorder.instance.Add_LoatTheWay(count);

        if (count != 0)
            desc = "\"眼前的建筑就如同是雾霾里的一个个模糊的影子,静静的淹没在这雾霾里\"\n"
                + TextColorSetter.Red("你的 " + count.ToString() + " 位同伴迷失了方向,再也没有回来");
        else
            desc = "\"在这浑浊的钢铁之都中,你努力的寻找着自己的方向\"";

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
