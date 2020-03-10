using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("当前游戏状态")]
    public bool gameStart;
    public bool gameEnd;
    public bool leaderAlive;

    [Space]
    public float cur_Count_Followers = 0;
    public float cur_Count_DeadBird = 0;

    void Awake()
    {
        instance = this;
        leaderAlive = true;
    }

    public void StartGame() {

    }
    public void EndGame() {

    }

    public void leaderBirdDead()
    {
        PlayerController.instance.ClearBird();
        leaderAlive = false;

        gameEnd = true;
    }

    public void Set_Count_Followers(int _v) {
        cur_Count_Followers = _v;
    }
    public void AddDeadBird() {
        cur_Count_DeadBird++;
    }
}
