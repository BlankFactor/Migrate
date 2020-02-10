using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("当前游戏状态")]
    public bool gameStart;

    [Space]
    public float cur_Count_Followers;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame() {

    }
    public void EndGame() {

    }

    public void Set_Count_Followers(int _v) {
        cur_Count_Followers = _v;
    }
}
