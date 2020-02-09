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
    public float farBorderPlane;
    [Range(0, 1)]
    public float nearBorderPlane;

    public FlyingBorderTrigger fbt;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Start()
    {
        base.Start();
        fbt.CreateTrigger(pivotDistance, pivotVerticalOffset, farBorderPlane);
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
            v.ChangeHeight(_posY + Random.Range(0,0.05f));
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
