using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController instance;

    [Header("设置")]
    public string text_TakeOff;
    public string text_Land;

    [Header("面板对象")]
    public Animator animator_MouseClickLeft;
    public TextDisplayer textDisplayer;

    [Header("其他对象")]
    public Text text;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
       //StartGame();
    }

    public void SetMouseClickLeft_FadeIn(bool value,bool _land) {
        if (value)
        {
            if (_land)
                text.text = text_Land;
            else
                text.text = text_TakeOff;
        }

       animator_MouseClickLeft.SetBool("FadeIn",value);
    }

    public void AddString(string _s) {
        textDisplayer.AddString(_s);
    }

    /// <summary>
    /// 通知GameManager游戏正式开始
    /// </summary>
    public void StartGame() {
        GameManager.instance.StartGame();
    }

}
