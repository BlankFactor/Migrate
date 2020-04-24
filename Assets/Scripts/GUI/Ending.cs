﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private Animator animator_Panel;
    public TextDisplayer_Ending ending;
    public Text text;

    private bool end;

    private List<string> str = new List<string>();

    private void Awake()
    {
        animator_Panel = GetComponent<Animator>();

    }

    public void FadeIn()
    {
        if(animator_Panel.gameObject.GetComponent<Image>().color.a != 1)
            animator_Panel.Play("Panel_Ending_FadeIn");
    }

    public void StartDisplayResult() {
        if (end) return;

        if (GameManager.instance.leaderAlive)
        {
            str.Add("日月星辰 昼夜晨昏");
            str.Add("经过了 " + WorldTimeManager.instance.days.ToString() + " 个日月" + "\n这趟旅途终于到达了尽头");
            str.Add(EventRecorder.instance.count_Hungry.ToString() + " 只鸟死于饥饿\n"
                + EventRecorder.instance.count_DirtyWater.ToString() + " 只鸟死于被污染的水源\n"
                + EventRecorder.instance.count_Poarching.ToString() + " 只鸟死于冷酷的枪下\n"
                + EventRecorder.instance.count_LostTheWay.ToString() + " 只鸟迷失在灰蒙的钢铁之都中\n");
            str.Add("环境破坏 偷猎者 污染");
            str.Add("使得原先庞大的队伍\n" + "变得屈指可数");
            str.Add("尽管途中充满了艰难险阻\n" + "但你终于到达了目的地");

            str.Add("策划 : 陆智恒 罗家伟\n"
                + "程序 : 罗家伟\n"
                + "美术 : 陆智恒\n"
                + "音乐来源 : bensound\n"
                + "音效来源 : 51miz\n");

            end = true;
        }
        else {
            str.Add("你死了 就这样 没做完");

            end = true;
        }

        ending.SetStrAndDisplay(str);
    }

    public void DisplayText() {
        text.gameObject.SetActive(true);
    }

    public void FadeOut() {
        animator_Panel.Play("Panel_Ending_FadeOut");
    }
}
