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
    public Clock panel_Clock;
    public EventDescription panel_EventDesc;

    [Header("其他对象")]
    public Text text;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
       StartGame();
    }

    #region Operation
    public void SetMouseClickLeft_FadeIn(bool value, bool _land)
    {
        if (value)
        {
            if (_land)
                text.text = text_Land;
            else
                text.text = text_TakeOff;
        }

        animator_MouseClickLeft.SetBool("FadeIn", value);
    }
    #endregion

    #region TextDisplayer
    public void AddString(string _s)
    {
        textDisplayer.AddString(_s);
    }
    #endregion


    #region Clock

    public void Display_Panel_Clock()
    {
        panel_Clock.gameObject.SetActive(true);
    }

    public void SetTimeline(object _timeline) {
        panel_Clock.SetTimeline(_timeline);
    }
    #endregion

    #region EventDesc
    public void Display_Panel_EventDesc(Sprite _s, string _t) {
        panel_EventDesc.DisplayWindow(_s, _t);
    }
    public void Disable_Panel_EventDesc() {
        panel_EventDesc.gameObject.SetActive(false);
    }
    #endregion

    /// <summary>
    /// 通知GameManager游戏正式开始
    /// </summary>
    public void StartGame() {
        GameManager.instance.StartGame();
    }

}
