﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LeaderBird : BBird
{
    [Header("鸟群控制设定")]
    public List<FollowerBird> birds;
    public LayerMask teamlessBirdLayer;

    [Header("鸟群飞行三角范围设定")]
    [Range(0,20)]
    public float pivotDistance;
    [Range(-10,10)]
    public float pivotVerticalOffset;
    [Range(0, 10)]
    public float farBorderPlane;
    [Range(0, 1)]
    public float nearBorderPlane;

    public FlyingBorderTrigger fbt;

    [Header("视野黑边")]
    public Volume vignette;
    [Space]
    public float energy_Threadhold = 20;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        GUIController.instance.ReflashEnergyBar(cur_CoreEnergy / coreEnergy, cur_Energy / coreEnergy);
    }

    public override void Start()
    {
        base.Start();
        fbt.CreateTrigger(pivotDistance, pivotVerticalOffset, farBorderPlane);
    }

    protected override void CheckEnergy()
    {
        base.CheckEnergy();


        if (alive && cur_Energy <= energy_Threadhold) {
            vignette.weight = (energy_Threadhold - cur_Energy) / energy_Threadhold;
            vignette.weight = Mathf.Clamp(vignette.weight, 0, 1);
        }
    }

    public override void TakeOff()
    {
        SearchTeamlessBird();
        if (lpd != null)
            Destroy(lpd.gameObject);
        base.TakeOff();

        foreach (var v in birds) {
            v.Command_TakeOff();
        }

        // 唤起UI
        GUIController.instance.SetMouseClickLeft_FadeIn(false,false);

        // 开始记录时间
        WorldTimeManager.instance.SetStop(false);
    }

    /// <summary>
    /// 寻找附近的无团队鸟 并将其加入队伍行列
    /// </summary>
    private void SearchTeamlessBird() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 10.0f, teamlessBirdLayer);
        if (cols.Length > 0) {
            GUIController.instance.AddString("更多的同伴加入到了队伍当中");
            GlobalAudioPlayer.instance.Play_Bell();

            foreach (var v in cols) {
                v.SendMessage("SetLeader", this);
            }
        }
    }

    public override void ChangeHeight(float _posY)
    {
        base.ChangeHeight(_posY);

        foreach (var v in birds)
        {
            v.ChangeHeight(_posY + Random.Range(0,0.05f));
        }
    }

    public override void SetSpeedUp(bool _v)
    {
        base.SetSpeedUp(_v);

        StartCoroutine(CallSpeedUp(_v));
    }

    IEnumerator CallSpeedUp(bool _v) {
        foreach (var v in birds)
        {
            v.SetSpeedUp(_v);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Land()
    {
        if (isLanding)
        {
            if (transform.position.Equals(landPos))
            {
                landed = true;
                isLanding = false;

                SetSpeedScale();

                // 受事件控制
                // 唤起UI
                //lpd.Action(this);
                //canTakeOff = true;
                //GUIController.instance.SetMouseClickLeft_FadeIn(true,false);

                // 改版后
                lpd.DeployTimeline();
                // *********
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, landPos, landSpeed * Time.fixedDeltaTime);
            }
        }
    }
    // 可起飞
    public void GrantToTakeOff() {
        canTakeOff = true;
        GUIController.instance.SetMouseClickLeft_FadeIn(true, false);
    }

    public override void SetLanding(LandPosData _lp)
    {
        base.SetLanding(_lp);

        foreach (var v in birds) {
            v.Command_Land(_lp);
        }
    }
    public void AddFollower(FollowerBird _fb) {
        birds.Add(_fb);
        GameManager.instance.Set_Count_Followers(birds.Count);
    }
    public void RemoveFollower(FollowerBird _fb) {
        birds.Remove(_fb);
        GameManager.instance.Set_Count_Followers(birds.Count);
        GameManager.instance.AddDeadBird();
    }

    /// <summary>
    /// 获取范围中的一个随机位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetClusterPos() {
        Vector2 pos = Vector2.zero;
        float x = Random.Range(nearBorderPlane, pivotDistance);
        pos.x -= x;
        pos.y = Random.Range(GetBottomBorder(x), GetTopBorder(x));

        return pos;
    }

    /// <summary>
    /// 获取三角边界上斜边最值
    /// </summary>
    /// <param name="_x"></param>
    /// <returns></returns>
    private float GetTopBorder(float _x) {
        Vector2 pivot = new Vector2(-pivotDistance, pivotVerticalOffset);

        Vector2 top = pivot;
        top.y += farBorderPlane;

        float a = top.y / top.x;

        return a * -_x;
    }
    /// <summary>
    /// 获取三角边界下斜边最值
    /// </summary>
    /// <param name="_x"></param>
    /// <returns></returns>
    private float GetBottomBorder(float _x) {
        Vector2 pivot = new Vector2(-pivotDistance, pivotVerticalOffset);

        Vector2 bottom = pivot;
        bottom.y -= farBorderPlane;

        float a = bottom.y / bottom.x;

        return a * -_x;
    }

    /// <summary>
    /// 检测跟随者所在位置的水平位置在飞行范围的什么位置
    /// 返回1则在右边
    /// 返回-1则在左边
    /// 返回0则水平分量恰好在范围内
    /// </summary>
    /// <returns></returns>
    public int CheckFollowerHorizonalCoord(Vector3 _pos)
    {
        Vector2 dir = _pos - transform.position;
        if (transform.position.x - pivotDistance > dir.x)
        {
            return -1;
        }
        else if(dir.x > transform.position.x){
            return 1;
        }

        return 0;
    }
    /// <summary>
    /// 检测跟随者所在位置的垂直位置在飞行范围的什么位置
    /// 返回1则在上
    /// 返回-1则在下
    /// 返回0则水平分量恰好在范围内
    /// </summary>
    /// <returns></returns>
    public int CheckFollowerVerticalCoord(Vector3 _pos)
    {
        float top;
        float bottom;

        float origin = transform.position.y;
        float topPoint = origin + farBorderPlane + pivotVerticalOffset;
        float bottomPoint = origin - farBorderPlane + pivotVerticalOffset;

        top = Mathf.Max(new float[] { origin, topPoint, bottomPoint });
        bottom = Mathf.Min(new float[] { origin, topPoint, bottomPoint });

        Vector2 dir = _pos - transform.position;
        if (dir.y < bottom)
        {
            return -1;
        }
        else if (dir.y > top)
        {
            return 1;
        }

        return 0;
    }

    public List<FollowerBird> GetFollowers() {
        return birds;
    }

     public override void Die()
    {
        isFlying = false;
        canTakeOff = false;

        alive = false;

        rid.bodyType = RigidbodyType2D.Dynamic;
        rid.gravityScale = 0.8f;

        // Destroy(gameObject, 5);

        GlobalAudioPlayer.instance.ChangeToBirdListener(false);

        GameManager.instance.leaderBirdDead();
        CameraManager.instance.ClearFollowTarget();
        StartCoroutine(RemoveVignetee());
    }

    private IEnumerator RemoveVignetee() {
        while (vignette.weight != 0) {
            vignette.weight -= Time.deltaTime * 0.5f;
            vignette.weight = Mathf.Clamp(vignette.weight, 0, 1);
            yield return Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 pivot = new Vector3(transform.position.x - pivotDistance, transform.position.y + pivotVerticalOffset);
        Vector2 top = pivot;
        top.y += farBorderPlane;
        Vector2 bottom = pivot;
        bottom.y -= farBorderPlane;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(-nearBorderPlane, GetTopBorder(nearBorderPlane),0), top);
        Gizmos.DrawLine(transform.position + new Vector3(-nearBorderPlane, -GetTopBorder(nearBorderPlane), 0), bottom);
        Gizmos.DrawLine(transform.position + new Vector3(-nearBorderPlane, GetTopBorder(nearBorderPlane), 0), transform.position + new Vector3(-nearBorderPlane, -GetTopBorder(0.3f), 0));
        Gizmos.DrawLine(top, bottom);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, pivot);
    }

}
