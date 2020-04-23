using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private Animator animator_Panel;
    public TextDisplayer_Ending ending;
    public Text text;

    private List<string> str = new List<string>();

    private void Awake()
    {
        animator_Panel = GetComponent<Animator>();

    }

    public void FadeIn()
    {
        animator_Panel.Play("Panel_Ending_FadeIn");
    }

    public void StartDisplayResult() {
        if (GameManager.instance.leaderAlive)
        {
            str.Add("测试1");
            str.Add("测试2");
            str.Add("测试3");
        }
        else {
            str.Add("Test1");
            str.Add("Test2");
            str.Add("Test3");
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
