﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBird : BBird
{
    [Header("当前飞行状态")]
    public Vector2 clusterPos;
    public LayerMask borderLayer;

    [Space]
    public float timer_ReflashClusterPos;

    public bool inPosition;
    public bool inBorder;

    [Header("跟随者属性")]
    [Range(0,2)]
    public float reflectionTime;
    [Range(0.01f, 0.1f)]
    public float actionableRadius = 0.05f;
    [Range(3,5)]
    public float reflashClusterTime;
    [Range(0, 1)]
    public float reflashProbability = 0.2f;

    [Range(1, 3)]
    public float speedUpScale = 1.5f;
    [Range(0,1)]
    public float speedDownScale = 0.5f;
    public LeaderBird leader;
    
    public override void Start()
    {
        base.Start();
        leader.AddFollower(this);
    }

    public override void Update()
    {
        base.Update();
        CheckInPosition();
        CalibratePos();
        ReflashClusterPos();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// 在飞行状态中校准飞行位置
    /// </summary>
    public void CalibratePos() {
        if (!isFlying) { return; }

        if (!inPosition)
        {
            float front = leader.transform.position.x + clusterPos.x + actionableRadius;
            float behind = leader.transform.position.x + clusterPos.x - actionableRadius;

            // 在目标点之后
            if (transform.position.x < behind)
            {
                SetSpeedScale(speedUpScale);
            }
            // 在目标点前
            else if (transform.position.x > front)
            {
                SetSpeedScale(speedDownScale);
            }
            // 抵达可活动区域
            else
            {
                inPosition = true;
                SetSpeedScale();
            }
        }
        // 已抵达活动范围 开始自由飞行
        else {

        }
        
    }

    /// <summary>
    /// 检测水平分量是否在可活动范围之内
    /// </summary>
    public void CheckInPosition() {
        float front = leader.transform.position.x + clusterPos.x + actionableRadius;
        float behind = leader.transform.position.x + clusterPos.x - actionableRadius;

        if (!(behind < transform.position.x && transform.position.x< front)) {
            inPosition = false;
        }
    }

    public override void ChangeHeight(float _posY)
    {
        if ( isFlying && !transform.position.y.Equals(_posY + clusterPos.y))
        {
            Vector2 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, _posY + clusterPos.y, 0.012f);
            //pos.y = Mathf.MoveTowards(pos.y, _posY + clusterPos.y, Time.deltaTime * climbSpeed);
            transform.position = pos;
        }
    }

    public void Command_TakeOff()
    {
        float time = Random.Range(0, reflectionTime);
        Invoke("TakeOff",time);

        clusterPos = leader.GetClusterPos();
    }

    public void Command_Land(Vector2 _pos) {
        float time = Random.Range(0, reflectionTime);
        SetLanding(_pos);
        Invoke("GrandLand", time);
    }

    public override void SetLanding(Vector2 _pos)
    {
        landPos = _pos;
    }

    public override void BirdDead()
    {
        leader.RemoveFollower(this);
    }

    private void GrandLand() {
        isFlying = false;
        isLanding = true;
    }

    private void SetInBorder(bool _v) {
        inBorder = _v;
    }

    /// <summary>
    /// 更新集群坐标
    /// </summary>
    private void ReflashClusterPos() {
        if (inPosition)
        {
            if (timer_ReflashClusterPos < 0 )
            {
                timer_ReflashClusterPos = Random.Range(reflashClusterTime / 2, reflashClusterTime);
                //clusterPos += 0.5f * (new Vector2(Mathf.Sin(Time.realtimeSinceStartup), Mathf.Cos(Time.time)));

                if(Random.Range(0, 1f) < reflashProbability)
                clusterPos = leader.GetClusterPos();
            }
            else
            {
                timer_ReflashClusterPos -= Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector2 v2 = leader.transform.position;
        Gizmos.DrawSphere(v2 + clusterPos, actionableRadius);
    }
}
