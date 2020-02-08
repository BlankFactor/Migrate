using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBird : BBird
{
    [Header("鸟群控制设定")]
    public List<FollowerBird> birds;

    [Header("鸟群飞行三角范围设定")]
    [Range(0,10)]
    public float pivotDistance;
    [Range(-5,5)]
    public float pivotVerticalOffset;
    [Range(0, 5)]
    public float borderHeight;

    public FlyingBorderTrigger fbt;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Start()
    {
        base.Start();
        fbt.CreateTrigger(pivotDistance, pivotVerticalOffset, borderHeight);
    }

    public override void TakeOff()
    {
        base.TakeOff();

        foreach (var v in birds) {
            v.Command_TakeOff();
        }
    }

    public override void ChangeHeight(float _posY)
    {
        base.ChangeHeight(_posY);

        foreach (var v in birds)
        {
            v.ChangeHeight(_posY);
        }
    }

    public override void SetSpeedUp(bool _v)
    {
        base.SetSpeedUp(_v);

        foreach (var v in birds) {
            v.SetSpeedUp(_v);
        }
    }

    public override void SetLanding(Vector2 _pos)
    {
        base.SetLanding(_pos);
        foreach (var v in birds) {
            v.Command_Land(_pos);
        }
    }
    public void AddFollower(FollowerBird _fb) {
        birds.Add(_fb);
    }
    public void RemoveFollower(FollowerBird _fb) {
        birds.Remove(_fb);
    }

    /// <summary>
    /// 获取范围中的一个随机位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetClusterPos() {
        Vector2 pos = Vector2.zero;
        float x = Random.Range(0, pivotDistance);
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
        top.y += borderHeight;

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
        bottom.y -= borderHeight;

        float a = bottom.y / bottom.x;

        return a * -_x;
    }

    private void OnDrawGizmos()
    {
        Vector2 pivot = new Vector3(transform.position.x - pivotDistance, transform.position.y + pivotVerticalOffset);
        Vector2 top = pivot;
        top.y += borderHeight;
        Vector2 bottom = pivot;
        bottom.y -= borderHeight;

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, top);
        Gizmos.DrawLine(transform.position, bottom);
        Gizmos.DrawLine(top, bottom);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, pivot);
    }

}
