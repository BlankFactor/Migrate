using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public static GUIController instance;

    private void Awake()
    {
        instance = this;
        
    }

    [Header("面板对象")]
    public GameObject panel_Beginning;

    private void Start()
    {
        StartGame();
    }

    /// <summary>
    /// 通知GameManager游戏正式开始
    /// </summary>
    public void StartGame() {
        GameManager.instance.StartGame();
    }

}
