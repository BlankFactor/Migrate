using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [Header("状态")]
    public bool enable;

    public Queue<string> stringQueue = new Queue<string>();

    [Header("对象")]
    public Animator animator;
    public Text text;

    public void AddString(string _s) {
        if(!stringQueue.Contains(_s))
            stringQueue.Enqueue(_s);

        if(!enable)
            Display();
    }

    public void Display() {
        if (stringQueue.Count == 0) {
            return;
        }
        enable = true;

        string s = stringQueue.Dequeue();
        text.text = s;

        animator.SetBool("FadeIn", true);
    }

    public void ResetEnable() {
        enable = false;

        animator.SetBool("FadeIn", false);
    }
}
